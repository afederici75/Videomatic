using Company.Videomatic.Application.Features.Artifacts.Commands;
using Company.Videomatic.Domain.Abstractions;
using Company.Videomatic.Domain.Aggregates.Artifact;
using Company.Videomatic.Domain.Specifications.Artifacts;
using Company.Videomatic.Domain.Specifications.Videos;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Commands;

public sealed class DeleteVideoHandler : IRequestHandler<DeleteVideoCommand, DeleteVideoResponse>
{
    public DeleteVideoHandler(IRepository<Video> repository)
    {
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public IRepository<Video> Repository { get; }

    public async Task<DeleteVideoResponse> Handle(DeleteVideoCommand request, CancellationToken cancellationToken = default)
    {
        var spec = new VideosByIdSpecification(new VideoId[] { request.Id });

        var Video = await Repository.FirstOrDefaultAsync(spec, cancellationToken);
        if (Video == null)
            return new DeleteVideoResponse(request.Id, false);

        await Repository.DeleteAsync(Video);

        return new DeleteVideoResponse(request.Id, true);        
    }
}
