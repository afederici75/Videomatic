using Microsoft.SemanticKernel.Memory;

namespace Infrastructure.SemanticKernel.Utilities;

public interface EmbeddingProducer
{
    public IEnumerable<MemoryRecord> ProduceEmbeddings(string input)
    { 
        return Array.Empty<MemoryRecord>();
    }
}

