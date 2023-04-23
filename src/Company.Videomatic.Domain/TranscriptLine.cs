using Newtonsoft.Json;

namespace Company.Videomatic.Domain;

public class TranscriptLine
{
    public int Id { get; init; }
    public string Text { get; init; }
    public TimeSpan? Duration { get; init; }
    public TimeSpan? StartsAt { get; init; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [JsonConstructor]
    private TranscriptLine()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        // For entity framework
    }

    public TranscriptLine(string text, TimeSpan? duration, TimeSpan? startsAt)
    {
        Text = text ?? throw new ArgumentNullException(nameof(text));
        Duration = duration;
        StartsAt = startsAt;
    }

    public override string ToString()
    {
        return $"[StartsAt:{StartsAt?.Seconds}, Duration:{Duration?.TotalSeconds}] '{Text}'";
    }
}
