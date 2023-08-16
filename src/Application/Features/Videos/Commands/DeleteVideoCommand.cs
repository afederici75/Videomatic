using SharedKernel.CQRS.Commands;
using Domain.Videos;

namespace Application.Features.Videos.Commands;

/// <summary>
/// This command is used to delete a video from the repository.
/// </summary>
/// <param name="VideoId"> The id of the video to delete. </param>
public class DeleteVideoCommand(VideoId id) : IRequest<Result>
{ 
    public VideoId Id { get; } = id;

    internal class Validator : AbstractValidator<DeleteVideoCommand>
    {
        public Validator()
        {
            RuleFor(x => (int)x.Id).GreaterThan(0);
        }
    }


    internal class Handler(IRepository<Video> repository, IMapper mapper) : DeleteEntityHandler<DeleteVideoCommand, Video, VideoId>(repository, mapper)
    {
    }

}