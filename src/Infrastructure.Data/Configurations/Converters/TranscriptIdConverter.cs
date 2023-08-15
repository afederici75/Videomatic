using Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data.Configurations.Converters;

public class TranscriptIdConverter : ValueConverter<TranscriptId, int>, IStronglyTypedIdConverter
{
    public TranscriptIdConverter()
        : base(id => id.Value,
               value => new TranscriptId(value))
    { }
}
