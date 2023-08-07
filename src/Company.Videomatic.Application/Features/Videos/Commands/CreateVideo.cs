using Company.SharedKernel.CQRS.Commands;

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
    ) : CreateEntityCommand<Video>();

internal class CreateVideoCommandValidator : AbstractValidator<CreateVideoCommand>
{
    public CreateVideoCommandValidator()
    {
        RuleFor(x => x.Location).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        //RuleFor(x => x.Description)
        RuleFor(x => x.Provider).NotEmpty();
        RuleFor(x => x.VideoPublishedAt).NotEmpty();
        RuleFor(x => x.ChannelId).NotEmpty();
        RuleFor(x => x.PlaylistId).NotEmpty();
        RuleFor(x => x.VideoOwnerChannelTitle).NotEmpty();
        RuleFor(x => x.VideoOwnerChannelId).NotEmpty();
    }
}