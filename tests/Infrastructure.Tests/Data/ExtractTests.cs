using Infrastructure.Tests.Data.Helpers;
using Newtonsoft.Json;
using SharedKernel.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace Infrastructure.Tests.Data;

[Collection("DbContextTests")]
public class ExtractTests : IClassFixture<DbContextFixture>//, IAsyncLifetime
{
    private const string VideoFileName = "D:\\elasticsearch\\Videos.ndjson";

    public ExtractTests(
        DbContextFixture fixture,
        ITestOutputHelperAccessor outputAccessor,
        ISender sender)
    {
        Output = outputAccessor?.Output ?? throw new ArgumentNullException(nameof(outputAccessor));
        Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        Sender = sender ?? throw new ArgumentNullException(nameof(sender));      
    }
  
    public ITestOutputHelper Output { get; }
    public DbContextFixture Fixture { get; }
    public ISender Sender { get; }


    [Fact]
    public async Task ExportVideosToJSONFile()
    {
        File.Delete(VideoFileName);

        List<Video> rows = await Fixture.DbContext.Videos.ToListAsync();
        var settings = new JsonSerializerSettings() 
        { 
            Formatting = Formatting.None,
        };

        var max = rows.Count;
        for (int i = 0; i < max; i++) 
        {
            var row = rows[i];

            string json = JsonConvert.SerializeObject(row, settings);
            json = RemoveNonPrintableCharacters(json);
            
            if (i < max - 1)
                json += "\r";

            //json = json.ToAlphaNumeric('\r', '\n', '-', 0x32);

            await File.AppendAllTextAsync(VideoFileName, json);            
        }
    }

    [Fact]
    public async Task ExportTranscriptsToJSONFile()
    {
        List<Transcript> rows = await Fixture.DbContext.Transcripts.ToListAsync();

        var output = new { Transcripts = rows };
        var json = JsonConvert.SerializeObject(output);

        File.WriteAllText("D:\\elasticsearch\\Transcripts.ndjson", json);
    }

    static string RemoveNonPrintableCharacters(string input)
    {
        // Create a character array to store the printable characters
        char[] printableChars = new char[input.Length];
        int printableCharCount = 0;

        // Iterate through the characters in the input string
        foreach (char c in input)
        {
            // Check if the character is a printable character
            if (IsPrintableCharacter(c))
            {
                // Add it to the printableChars array
                printableChars[printableCharCount] = c;
                printableCharCount++;
            }
        }

        // Create a new string with only the printable characters
        string cleanedString = new string(printableChars, 0, printableCharCount);

        return cleanedString;
    }

    static bool IsPrintableCharacter(char c)
    {
        // Check if the character is a printable character
        // Printable characters have ASCII values from 0x20 to 0x7E
        return (c >= 0x20 && c <= 0x7E) || (c == 0x10) || (c == 0x13);
    }
}

public static partial class JsonExtensions
{
    public static void ToNewLineDelimitedJson<t>(Stream stream, IEnumerable<t> items)
    {
        // let caller dispose the underlying stream 
        using (var textwriter = new StreamWriter(stream, new UTF8Encoding(false, true), 1024, true))
        {
            ToNewLineDelimitedJson(textwriter, items);
        }
    }

    public static void ToNewLineDelimitedJson<T>(TextWriter textwriter, IEnumerable<T> items)
    {
        var serializer = JsonSerializer.CreateDefault();

        foreach (var item in items)
        {
            // formatting.none is the default; i set it here for clarity.
            using (var writer = new JsonTextWriter(textwriter) { Formatting = Formatting.None, CloseOutput = false })
            {
                serializer.Serialize(writer, item);
            }
            // https://web.archive.org/web/20180513150745/http://specs.okfnlabs.org/ndjson/
            // each json text must conform to the [rfc7159] standard and must be written to the stream followed by the newline character \n (0x0a). 
            // the newline charater may be preceeded by a carriage return \r (0x0d). the json texts must not contain newlines or carriage returns.
            textwriter.Write("\n");
        }
    }
}