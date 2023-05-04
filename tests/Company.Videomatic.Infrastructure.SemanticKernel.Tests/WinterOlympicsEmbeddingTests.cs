using CsvHelper;

using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel.AI.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI.TextEmbedding;
using Microsoft.SemanticKernel.Memory;

using System.Globalization;
using System.Runtime.CompilerServices;

using Xunit.Abstractions;

namespace Company.Videomatic.Infrastructure.SemanticKernel.Tests;

public class WinterOlympicsFixture : IAsyncLifetime
{
    public const string CollectionName = "winterOlympics";

    public WinterOlympicsFixture(IMemoryStore memoryStore)
    {
        MemoryStore = memoryStore ?? throw new ArgumentNullException(nameof(memoryStore));
    }

    public IMemoryStore MemoryStore { get; }

    public Task DisposeAsync() => Task.CompletedTask;

    public async Task InitializeAsync()
    {
        // TODO: how to support cancellations?
            
        await MemoryStore.CreateCollectionAsync(CollectionName);

        await foreach (var rec in LoadLocalEmbeddingsAsync(MemoryStore))
        {
            await MemoryStore.UpsertAsync(CollectionName, rec);
        }
    }

    const string CsvUrl = "https://cdn.openai.com/API/examples/data/winter_olympics_2022.csv";
    const string CsvFileName = @"TestData\winter_olympics_2022.csv";

    async Task DownloadHugeCsvIfNecessary(CancellationToken cancellationToken)
    {
        if (File.Exists(CsvFileName))
            return;

        using var client = new System.Net.Http.HttpClient();

        using (var stream = await client.GetStreamAsync(CsvUrl, cancellationToken))
        {
            using var fileStream = File.Create(CsvFileName);

            await stream.CopyToAsync(fileStream, cancellationToken);
        }
    }

    async IAsyncEnumerable<MemoryRecord> LoadLocalEmbeddingsAsync(IMemoryStore store, [EnumeratorCancellation] CancellationToken cancellation = default)
    {
        // Reads the 209Mb file of the example https://github.com/openai/openai-cookbook/blob/main/examples/Question_answering_using_embeddings.ipynb?ref=mlq.ai
        await DownloadHugeCsvIfNecessary(cancellation);

        using var csvStream = new StreamReader(CsvFileName);
        using var csvReader = new CsvReader(csvStream, CultureInfo.InvariantCulture);

        await foreach (var rec in csvReader.GetRecordsAsync<CsvEmbeddingRecord>())
        {
            yield return rec.ToMemoryRecord();
        }

        // Alternative option
        //var tmp = LoadLocalEmbeddingsAsync(memStore, cancellation);
        //await memStore.UpsertBatchAsync(CollectionName, tmp, cancellation);
    }

}

public partial class WinterOlympicsEmbeddingTests : IClassFixture<WinterOlympicsFixture>
{
    SemanticKernelOptions Configuration { get; }
    WinterOlympicsFixture Fixture { get; }
    ITestOutputHelper Output { get; }

    public WinterOlympicsEmbeddingTests(
        WinterOlympicsFixture fixture, 
        IOptions<SemanticKernelOptions> configuration, 
        ITestOutputHelper output)
    {
        Configuration = configuration?.Value ?? throw new ArgumentNullException(nameof(configuration));
        Fixture = fixture;
        Output = output ?? throw new ArgumentNullException(nameof(output));
    }

    
    [Theory]
    [InlineData("Which athletes won the gold medal in curling at the 2022 Winter Olympics?", true, null)]
    [InlineData("who winned gold metals in kurling at the olimpics", true, null)] // misspelled question
    [InlineData("How many records were set at the 2022 Winter Olympics?", true, null)] // counting question SOMEHOW I GET NO COUNT? Model issue?
    [InlineData("Did Jamaica or Cuba have more athletes at the 2022 Winter Olympics?", true, null)] // comparison question
    [InlineData("What is 2+2?", false, null)] // question outside of the scope
    [InlineData("Which Olympic sport is the most entertaining?", false, null)] // subjective question
    [InlineData("Who won the gold medal in curling at the 2018 Winter Olympics?", false, null)] // question outside of the scope  
    public async Task LoadWinterOlympicsData(string question, bool shouldHaveAnswer, CancellationToken cancellation)
    {
        // This tests replicates what is at https://github.com/openai/openai-cookbook/blob/main/examples/Question_answering_using_embeddings.ipynb?ref=mlq.ai
        // Look at the fixture for initilization and more

        // Creates our ASK
        OpenAITextEmbeddingGeneration gen = new (Configuration.EmbeddingModel, Configuration.ApiKey);
        ISemanticTextMemory memory = new SemanticTextMemory(Fixture.MemoryStore, gen);

        var id = 0;
        var semanticMatches = string.Empty;
        await foreach (var localMatch in memory.SearchAsync(WinterOlympicsFixture.CollectionName, question, limit: 5))
        {
            Output.WriteLine($"Semantic result #{id++}, Relevance: {localMatch.Relevance}.");
            semanticMatches += $"\n\nWikipedia article section:\n{localMatch.Metadata.Text}\n";            
        }
        
        // Ask the question        
        IChatCompletion chatCompletion = new OpenAIChatCompletion(Configuration.Model, Configuration.ApiKey);
        ChatHistory newChat = chatCompletion.CreateNewChat(
            instructions: "You answer questions about the 2022 Winter Olympics.");

        var ask  = "Use the below articles on the 2022 Winter Olympics to answer the subsequent question. " +
                   "If the answer cannot be found in the articles, write 'I could not find an answer.'\n" + 
                   semanticMatches + '\n' +
                   $"Question: {question}";

        newChat.AddMessage(ChatHistory.AuthorRoles.User, ask);

        string response = await chatCompletion.GenerateMessageAsync(newChat, new ChatRequestSettings
        {
            Temperature = 0
        });

        Output.WriteLine("------------------------------------------------------");
        Output.WriteLine($"QUESTION: {question}");        
        Output.WriteLine($"RESPONSE:\n{response}");
        Output.WriteLine($"SEMANTIC MATCHES:\n{semanticMatches}");
        Output.WriteLine("------------------------------------------------------");

        if (!shouldHaveAnswer) 
        {
            response.Should().Contain("I could not find an answer.");
        }
        else
        {
            response.Length.Should().BeGreaterThan(50); 
        }


    }
}
