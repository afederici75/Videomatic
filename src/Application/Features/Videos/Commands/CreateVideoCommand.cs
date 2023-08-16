using SharedKernel.CQRS.Commands;
using Domain.Videos;

namespace Application.Features.Videos.Commands;

public class CreateVideoCommand(
    string location,
    string name,
    string? description,
    string provider,
    DateTime videoPublishedAt,
    string channelId,
    string playlistId,
    string videoOwnerChannelTitle,
    string videoOwnerChannelId,
    string thumbnailUrl,
    string pictureUrl
    ) : IRequest<Result<Video>>
{
    public string Location { get; } = location;
    public string Name { get; } = name;
    public string? Description { get; } = description;
    public string Provider { get; } = provider;
    public DateTime VideoPublishedAt { get; } = videoPublishedAt;
    public string ChannelId { get; } = channelId;
    public string PlaylistId { get; } = playlistId;
    public string VideoOwnerChannelTitle { get; } = videoOwnerChannelTitle;
    public string VideoOwnerChannelId { get; } = videoOwnerChannelId;
    public string ThumbnailUrl { get; } = thumbnailUrl;
    public string PictureUrl { get; } = pictureUrl;

    internal class Validator : AbstractValidator<CreateVideoCommand>
    {
        public Validator()
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


    internal class Handler : CreateEntityHandler<CreateVideoCommand, Video>
    {
        public Handler(IRepository<Video> repository, IMapper mapper) : base(repository, mapper)
        {

        }
    }

}

