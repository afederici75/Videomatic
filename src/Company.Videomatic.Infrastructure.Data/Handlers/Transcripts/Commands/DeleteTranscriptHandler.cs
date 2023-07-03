using Company.Videomatic.Application.Features.Artifacts.Commands;
using Company.Videomatic.Application.Features.Transcripts.Commands;
using Company.Videomatic.Domain.Aggregates.Artifact;
using Company.Videomatic.Domain.Aggregates.Transcript;
using Company.Videomatic.Domain.Specifications.Artifacts;
using Company.Videomatic.Domain.Specifications.Transcripts;

namespace Company.Videomatic.Infrastructure.Data.Handlers.Transcripts.Commands;

public class DeleteTranscriptHandler : IRequestHandler<DeleteTranscriptCommand, DeleteTranscriptResponse>
{
    public DeleteTranscriptHandler(IRepository<Transcript> repository)
    {
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public IRepository<Transcript> Repository { get; }
    public async Task<DeleteTranscriptResponse> Handle(DeleteTranscriptCommand request, CancellationToken cancellationToken)
    {
        var spec = new TranscriptByIdsSpecification(new TranscriptId[] { request.Id });

        var Transcript = await Repository.FirstOrDefaultAsync(spec, cancellationToken);
        if (Transcript == null)
            return new DeleteTranscriptResponse(request.Id, false);

        await Repository.DeleteAsync(Transcript);

        return new DeleteTranscriptResponse(request.Id, true);
    }
}
