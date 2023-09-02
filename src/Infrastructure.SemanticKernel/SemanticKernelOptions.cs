namespace Infrastructure.SemanticKernel;

public class SemanticKernelOptions
{
    public string Model { get; set; } = "text-davinci-003";
    public string EmbeddingModel { get; set; } = "text-embedding-ada-002";
    public string ChatModel { get; set; } = "gpt-3.5-turbo";
    public required string ApiKey { get; set; }
    public string? Organization { get; set; } = string.Empty;
    public string? MemoryStoreEndpoint { get; set; }
    public string? MemoryStoreApiKey { get; set; }
}