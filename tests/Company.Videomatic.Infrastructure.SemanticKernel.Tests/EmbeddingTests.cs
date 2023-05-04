using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using JetBrains.Annotations;
using Microsoft.SemanticKernel.AI.Embeddings;
using Microsoft.SemanticKernel.Memory;
using Newtonsoft.Json;
using System.Globalization;

namespace Company.Videomatic.Infrastructure.SemanticKernel.Tests;


public class EmbeddingTests
{
    public class EmbeddingRecord
    {
        [Name("text")]
        [Index(0)]
        public required string Text { get; init; }

        [Name("embedding")]
        [Index(1)]
        public required string ValuesArray { get; init; }

        public MemoryRecord ToMemoryRecord()
        {
            var key = "1";

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
     
    [Fact]
    public async Task LoadWinterOlympicsData()
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {            
            HasHeaderRecord = true,            
        };

        using var reader = new StreamReader(@"TestData\winter_olympics_2022.csv");
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        //var header = csv.ReadHeader(); // Somehow this blows up

        List<EmbeddingRecord> records = csv.GetRecords<EmbeddingRecord>()
            .Take(10)
            .ToList();

        records.Should().HaveCount(10);

        var memRecords = records
            .Select(x => x.ToMemoryRecord())
            .ToList();
        
    }
}
