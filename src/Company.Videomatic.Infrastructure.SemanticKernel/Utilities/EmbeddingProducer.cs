using Microsoft.SemanticKernel.Memory;

namespace Company.Videomatic.Infrastructure.SemanticKernel.Utilities;

public interface EmbeddingProducer
{
    public IEnumerable<MemoryRecord> ProduceEmbeddings(string input)
    { 
        return Array.Empty<MemoryRecord>();
    }
}

