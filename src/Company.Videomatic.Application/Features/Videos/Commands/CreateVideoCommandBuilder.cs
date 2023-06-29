namespace Company.Videomatic.Application.Features.Videos.Commands;

public class CreateVideoCommandBuilder
{
    public CreateVideoCommand WithEmptyVideoDetails(
        string location,
        string name,
        string? description)
    {
        const string None = "None";

        return new CreateVideoCommand(
            Location: location,
            Name: name,
            Description: description,
            Provider: None,
            VideoPublishedAt: DateTime.UtcNow,
            ChannelId: None,
            PlaylistId: None,
            VideoOwnerChannelTitle: None,
            VideoOwnerChannelId: None);
    }

        public CreateVideoCommand WithRandomValuesAndEmptyVideoDetails([System.Runtime.CompilerServices.CallerMemberName] string textId = "")
        {
            string location = $"https://www.youtube.com/watch?v=#VideoId{textId}";
            string name = $"VideoName{textId}";
            string? description = $"The description of video {textId}";

            return WithEmptyVideoDetails(location, name, description);
        }
}
