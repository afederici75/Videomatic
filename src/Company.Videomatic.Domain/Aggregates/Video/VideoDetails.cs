namespace Company.Videomatic.Domain.Aggregates.Video;

public record VideoDetails(
    string Provider,
    string ProviderVideoId,
    DateTime VideoPublishedAt,
    //string ChannelId,
    //string PlaylistId,
    //int Position,
    string VideoOwnerChannelTitle,
    string VideoOwnerChannelId)
{
    private VideoDetails() :
        this(Provider: "NONE",
             ProviderVideoId: "NONE",
             VideoPublishedAt: DateTime.UtcNow,
             //ChannelId: "",
             //PlaylistId: "",
             //Position: 0,
             VideoOwnerChannelTitle: "",
             VideoOwnerChannelId: "")
    { }

    public static VideoDetails CreateEmpty()
    {
        return new VideoDetails();
    }
}
