namespace Company.Videomatic.Domain.Aggregates.Video;

public record VideoDetails(
    string Provider,
    string ProviderVideoId,
    DateTime VideoPublishedAt,
    string VideoOwnerChannelTitle,
    string VideoOwnerChannelId)
{
    public static VideoDetails CreateEmpty()
    {
        return new VideoDetails(
            Provider: "NONE",
            ProviderVideoId: "NONE",
            VideoPublishedAt: DateTime.UtcNow,
            VideoOwnerChannelTitle: "",
            VideoOwnerChannelId: "");
    }
}
