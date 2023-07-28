using AutoMapper;
using Company.SharedKernel.Abstractions;

namespace Company.Videomatic.Application.Handlers.Artifacts.Commands;

public class DeleteArtifactHandler : DeleteAggregateRootHandler<DeleteArtifactCommand, Artifact, ArtifactId>
{
    public DeleteArtifactHandler(IRepository<Artifact> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    protected override ArtifactId ConvertIdOfRequest(DeleteArtifactCommand request) => request.Id;
}
