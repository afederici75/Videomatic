using CsvHelper.Configuration.Attributes;
using Microsoft.SemanticKernel.AI.Embeddings;
using Microsoft.SemanticKernel.Memory;
using Newtonsoft.Json;

namespace Company.Videomatic.Infrastructure.SemanticKernel.Tests;

public class CsvEmbeddingRecord
{
    [Name("text")]
    [Index(0)]
    public required string Text { get; init; }

    [Name("embedding")]
    [Index(1)]
    public required string ValuesArray { get; init; }

    static int _key = 0;

    public MemoryRecord ToMemoryRecord()
    {
        var key = "PK_" + _key++.ToString();

        float[]? floatValues = JsonConvert.DeserializeObject<float[]>(this.ValuesArray);
        Embedding<float> e = (floatValues != null) ? new Embedding<float>(floatValues) : new();

        MemoryRecordMetadata meta = new(
            isReference: true,
            id: key,
            text: Text,
            description: null,
            externalSourceName: null,
            additionalMetadata: null);

        return new MemoryRecord(meta, e, null);
    }
}