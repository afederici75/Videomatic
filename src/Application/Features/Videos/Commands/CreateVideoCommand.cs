using SharedKernel.CQRS.Commands;
using Domain.Videos;

namespace Application.Features.Videos.Commands;

public class CreateVideoCommand(
    string location,
    string name,
    string? description,
    string providerId,
    DateTime publishedAt,
    string channelId,
    string playlistId,
    string ownerChannelTitle,
    string ownerChannelId,
    string thumbnailUrl,
    string pictureUrl
    ) : IRequest<Result<Video>>
{
    public string Location { get; } = location;
    public string Name { get; } = name;
    public string? Description { get; } = description;
    public string ProviderId { get; } = providerId;
    public DateTime PublishedOn { get; } = publishedAt;
    public string ChannelId { get; } = channelId;
    public string PlaylistId { get; } = playlistId;
    public string ChannelName { get; } = ownerChannelTitle;
    //public string OwnerChannelId { get; } = ownerChannelId;
    public string ThumbnailUrl { get; } = thumbnailUrl;
    public string PictureUrl { get; } = pictureUrl;

    internal class Validator : AbstractValidator<CreateVideoCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Location).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            //RuleFor(x => x.Description)
            RuleFor(x => x.ProviderId).NotEmpty();
            RuleFor(x => x.PublishedOn).NotEmpty();
            RuleFor(x => x.ChannelId).NotEmpty();
            RuleFor(x => x.PlaylistId).NotEmpty();
            RuleFor(x => x.ChannelName).NotEmpty();
            //RuleFor(x => x.OwnerChannelId).NotEmpty();
            RuleFor(x => x.PictureUrl).NotEmpty();
            RuleFor(x => x.ThumbnailUrl).NotEmpty();
        }
    }


    internal class Handler(IRepository<Video> repository, IMapper mapper) : CreateEntityHandler<CreateVideoCommand, Video>(repository, mapper)
    {
    }

}

