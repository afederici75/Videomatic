namespace Company.Videomatic.Domain.Aggregates.Transcript;

public record TranscriptId(long Value = 0) : ILongId
{
    public static implicit operator long(TranscriptId x) => x.Value;
    public static implicit operator TranscriptId(long x) => new (x);
}

