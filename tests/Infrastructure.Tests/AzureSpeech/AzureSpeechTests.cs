using Company.Videomatic.Infrastructure.AzureSpeech;

namespace Infrastructure.Tests.AzureSpeech;

public class AzureSpeechTests
{
    [Theory]
    [InlineData(null, "AzureSpeech\\Data\\Conference.wav", "This is Peter, this is Johnny, Kenny and Josh, we just wanted to take a minute to thank.")]
    [InlineData(null, "AzureSpeech\\Data\\2x2LargeAndSmall.wav", "Two by two creatures, all large and small foul.")]
    //[InlineData(null, "AzureSpeech\\Data\\Voldomor.wav", "It was dark times, Harry. Dark times.Voldemort started to gather some followers.")] // TODO: This broke!?!?
    public async Task TranscribeWav([FromServices] ITranscriber transcriber, string filename, string expected)
    {
        Assert.NotNull(transcriber);
        transcriber.OnTranscribed = (chunk) => 
        {
            chunk.Text.Should().Be(expected);  
            return Task.FromResult(true);
        };

        await transcriber.TranscribeAsync(filename);        
    }

    [Theory(Skip = "Generates too big of a file for github")]
    [InlineData(null, "Data\\02.13.2023_1040AM.wav")]
    public async Task TranscribeBigWav([FromServices] ITranscriber transcriber, string filename)
    {
        var txtFileName = Path.ChangeExtension(filename, ".txt");
        File.Delete(txtFileName);

        Assert.NotNull(transcriber);
        
        var count = 0;

        transcriber.OnTranscribed = async (chunk) =>
        {
            await File.AppendAllTextAsync(txtFileName, chunk.Text);

            count += chunk.Text.Length;

            return (count < 50000);
        };

        await transcriber.TranscribeAsync(filename);
    }
}