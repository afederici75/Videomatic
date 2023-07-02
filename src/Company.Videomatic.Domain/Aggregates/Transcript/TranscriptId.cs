namespace Company.Videomatic.Domain.Aggregates.Transcript;

public record TranscriptId(long Value = 0)
{
    public static implicit operator long(TranscriptId x) => x.Value;
    public static implicit operator TranscriptId(long x) => new TranscriptId(x);
}

