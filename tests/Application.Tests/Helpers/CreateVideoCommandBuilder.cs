namespace Application.Tests.Helpers;

public class CreateVideoCommandBuilder
{
    /// <summary>
    /// Static factory method that requires no state.
    /// </summary>
    public static CreateVideoCommand WithEmptyVideoDetails(
        string location,
        string name,
        string? description)
    {
        const string None = "None";

        return new CreateVideoCommand(
            location: location,
            name: name,
            description: description,
            providerId: None,
            publishedAt: DateTime.UtcNow,
            channelId: None,
            playlistId: None,
            ownerChannelTitle: None,
            ownerChannelId: None,
            thumbnailUrl: "http://1",
            pictureUrl: "http://2");
    }

    /// <summary>
    /// Static factory method that requires no state.
    /// </summary>
    /// <param name="textId"></param>
    /// <returns></returns>
    public static CreateVideoCommand WithRandomValuesAndEmptyVideoDetails([System.Runtime.CompilerServices.CallerMemberName] string textId = "")
    {
        string location = $"https://www.youtube.com/watch?v=#VideoId{textId}";
        string name = $"VideoName{textId}";
        string? description = $"The description of video {textId}";

        return WithEmptyVideoDetails(location, name, description);
    }
}
