namespace Company.Videomatic.Domain.Aggregates.Video;

public record VideoDetails(
    string Provider,
    string ProviderVideoId,
    DateTime VideoPublishedAt,
    string VideoOwnerChannelTitle,
    string VideoOwnerChannelId)
{
    // Factory methods.
    public static VideoDetails CreateEmpty()
    {
        return new VideoDetails();
    }

    // Automapper.
    private VideoDetails() : this(
        Provider: "NONE",
        ProviderVideoId: "NONE",
        VideoPublishedAt: DateTime.UtcNow,
        VideoOwnerChannelTitle: "",
        VideoOwnerChannelId: "") { }    
}