namespace Infrastructure.Data.Configurations.Converters;

[StronglyTypedIdConverter]
public class TranscriptIdConverter : ValueConverter<TranscriptId, int>
{
    public TranscriptIdConverter()
        : base(id => id.Value,
               value => new TranscriptId(value))
    { }
}
