namespace Application.Features.Videos.Commands;

/// <summary>
/// This command is used to update a video in the repository.
/// </summary>
public class UpdateVideoCommand(
    VideoId Id,
    string Name,
    string? Description = default) : IRequest<Result<Video>>
{
    public VideoId Id { get; } = Id;
    public string Name { get; } = Name;
    public string? Description { get; } = Description;


    internal class Validator : AbstractValidator<UpdateVideoCommand>
    {
        public Validator()
        {
            RuleFor(x => (int)x.Id).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
        }
    }

    internal class Handler(IMyRepository<Video> repository, IMapper mapper) : UpdateEntityHandler<UpdateVideoCommand, Video, VideoId>(repository, mapper)
    {
    }

}