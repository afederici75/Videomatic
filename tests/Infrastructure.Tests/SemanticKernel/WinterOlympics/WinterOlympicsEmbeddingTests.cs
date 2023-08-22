using Infrastructure.SemanticKernel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel.AI.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI.TextEmbedding;
using Microsoft.SemanticKernel.Memory;

using Xunit.Abstractions;

namespace Infrastructure.Tests.SemanticKernel;

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


    [Theory(Skip = "Skip for now")]
    [InlineData("Which athletes won the gold medal in curling at the 2022 Winter Olympics?", true)]
    [InlineData("who winned gold metals in kurling at the olimpics", true)] // misspelled question
    [InlineData("How many records were set at the 2022 Winter Olympics?", true)] // counting question SOMEHOW I GET NO COUNT? Model issue?
    [InlineData("Did Jamaica or Cuba have more athletes at the 2022 Winter Olympics?", true)] // comparison question
    [InlineData("What is 2+2?", false)] // question outside of the scope
    [InlineData("Which Olympic sport is the most entertaining?", false)] // subjective question
    [InlineData("Who won the gold medal in curling at the 2018 Winter Olympics?", false)] // question outside of the scope  
    public async Task LoadWinterOlympicsData(string question, bool shouldHaveAnswer)
    {
        // This tests replicates what is at https://github.com/openai/openai-cookbook/blob/main/examples/Question_answering_using_embeddings.ipynb?ref=mlq.ai
        // Look at the fixture for initilization and more

        // Creates our ASK
        OpenAITextEmbeddingGeneration gen = new(Configuration.EmbeddingModel, Configuration.ApiKey);
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

        var ask = "Use the below articles on the 2022 Winter Olympics to answer the subsequent question. " +
                   "If the answer cannot be found in the articles, write 'I could not find an answer.'\n" +
                   semanticMatches + '\n' +
                   $"Question: {question}";

        newChat.AddMessage(AuthorRole.User, ask);

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
