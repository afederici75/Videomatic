using static System.Net.WebRequestMethods;

namespace Company.Videomatic.Application.Features.Videos.Commands;

public record CreateVideoCommand(
    string Location,
    string Name,
    string? Description,
    string Provider,
    DateTime VideoPublishedAt,
    string ChannelId,
    string PlaylistId,
    string VideoOwnerChannelTitle,
    string VideoOwnerChannelId
    ) : IRequest<CreatedResponse> { }

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

    public CreateVideoCommand WithRandomValuesAndEmptyVideoDetails(string id)
    {
        string location = $"https://www.youtube.com/watch?v=#VideoId{id}";
        string name = $"Name{id}";
        string? description = $"The description of video {id}";

        return WithEmptyVideoDetails(location, name, description);
    }
}

internal class CreateVideoCommandValidator : AbstractValidator<CreateVideoCommand>
{
    public CreateVideoCommandValidator()
    {
        RuleFor(x => x.Location).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        //RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Provider).NotEmpty();
        RuleFor(x => x.VideoPublishedAt).NotEmpty();
        RuleFor(x => x.ChannelId).NotEmpty();
        RuleFor(x => x.PlaylistId).NotEmpty();
        RuleFor(x => x.VideoOwnerChannelTitle).NotEmpty();
        RuleFor(x => x.VideoOwnerChannelId).NotEmpty();
    }
}