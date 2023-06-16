namespace Company.Videomatic.Domain.Model;

public class TranscriptLine : EntityBase
{
    public string Text { get; private set; }
    public TimeSpan Duration { get; private set; }
    public TimeSpan StartsAt { get; private set; }

    public TranscriptLine(string text, TimeSpan duration, TimeSpan startsAt)
    {
        Text = Guard.Against.NullOrWhiteSpace(text, nameof(text));
        Duration = Guard.Against.NegativeOrZero(duration, nameof(duration));
        StartsAt = Guard.Against.NegativeOrZero(startsAt, nameof(startsAt));
    }
    public override string ToString()
    {
        return $"'{Text}' [StartsAt:{StartsAt.Seconds}, Duration:{Duration.TotalSeconds}]";
    }

    #region Private 

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [JsonConstructor]
    private TranscriptLine()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        // For entity framework
    }

    #endregion    
}
