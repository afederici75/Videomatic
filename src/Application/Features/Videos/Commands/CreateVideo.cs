using SharedKernel.CQRS.Commands;
using Domain.Videos;

namespace Application.Features.Videos.Commands;

public readonly record struct CreateVideoCommand(
    string Location,
    string Name,
    string? Description,
    string Provider,
    DateTime VideoPublishedAt,
    string ChannelId,
    string PlaylistId,
    string VideoOwnerChannelTitle,
    string VideoOwnerChannelId,
    string ThumbnailUrl,
    string PictureUrl
    ) : IRequest<Result<Video>>;

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
        RuleFor(x => x.PictureUrl).NotEmpty();
        RuleFor(x => x.ThumbnailUrl).NotEmpty();
    }
}