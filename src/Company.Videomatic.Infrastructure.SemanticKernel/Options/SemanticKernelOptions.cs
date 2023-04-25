namespace Company.Videomatic.Infrastructure.SemanticKernel.Options;

public class SemanticKernelOptions
{
    public string Model { get; set; } = "text-davinci-002";
    public string EmbeddingModel { get; set; } = "text-embedding-ada-002";
    public required string ApiKey { get; set; }
    public string? Organization { get; set; } = string.Empty;
}