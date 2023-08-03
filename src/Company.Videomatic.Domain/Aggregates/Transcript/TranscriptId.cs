namespace Company.Videomatic.Domain.Aggregates.Transcript;

public record TranscriptId(int Value = 0)
{
    public static implicit operator int(TranscriptId x) => x.Value;
    public static implicit operator TranscriptId(int x) => new (x);
}

