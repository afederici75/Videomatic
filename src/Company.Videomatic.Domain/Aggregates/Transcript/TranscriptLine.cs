namespace Company.Videomatic.Domain.Aggregates.Transcript;

public class TranscriptLine : ValueObject
{
    public static implicit operator string(TranscriptLine x) => x.Text;
    public static implicit operator TranscriptLine(string x) => new (x, null, null);
    
    public TranscriptLine(string text, TimeSpan? duration = null, TimeSpan? startsAt = null)
    {
        Text = text;
        Duration = duration;
        StartsAt = startsAt;        
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Text;
        yield return Duration ?? TimeSpan.MinValue;
        yield return StartsAt ?? TimeSpan.MinValue; 
    }

    public string Text { get; private set; } = default!;
    public TimeSpan? Duration { get; private set; }
    public TimeSpan? StartsAt { get; private set; }

    #region

    private TranscriptLine()
    { }

    #endregion
}

