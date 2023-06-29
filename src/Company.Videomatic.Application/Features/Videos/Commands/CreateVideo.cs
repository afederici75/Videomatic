using Company.Videomatic.Application.Features.DataAccess;

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
    ) : IRequest<CreatedResponse>
{
    public CreateVideoCommand(
        string Location,
        string Name,
        string? Description) : this(Location, Name, Description, "", DateTime.UtcNow, "", "", "", "")
    {
        
    }
}

internal class CreateVideoCommandValidator : AbstractValidator<CreateVideoCommand>
{
    public CreateVideoCommandValidator()
    {
        RuleFor(x => x.Location).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}