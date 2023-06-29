using Company.Videomatic.Application.Features.DataAccess;

namespace Company.Videomatic.Application.Features.Videos.Commands;

public record CreateVideoDetails(
    string Provider,
    DateTime VideoPublishedAt,
    string ChannelId,
    string PlaylistId,
    int Position,
    string VideoOwnerChannelTitle,
    string VideoOwnerChannelId
    )
{
    public static CreateVideoDetails CreateDummy()
    {
        return new CreateVideoDetails(
                       Provider: "YOUTUBE",
                       VideoPublishedAt: DateTime.UtcNow,
                       ChannelId: "UCX6OQ3DkcsbYNE6H8uQQuVA",
                       PlaylistId: "PL3ZslI15yo2qZjZsX2WpZK4Q9tqEbY9Y_",
                       Position: 321,
                       VideoOwnerChannelTitle: "Microsoft Developer",
                       VideoOwnerChannelId: "UCsMica-v34Irf9KVTh6xx-g");
    }
}

public record CreateVideoCommand(
    string Location,
    string Name,
    string? Description,
    CreateVideoDetails? Details
    ) : IRequest<CreatedResponse>
{    
}

internal class CreateVideoCommandValidator : AbstractValidator<CreateVideoCommand>
{
    public CreateVideoCommandValidator()
    {
        RuleFor(x => x.Location).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}