namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Commands;

public sealed class DeleteVideoHandler : BaseRequestHandler<DeleteVideoCommand, DeleteVideoResponse>
{
    public DeleteVideoHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<DeleteVideoResponse> Handle(DeleteVideoCommand request, CancellationToken cancellationToken = default)
    {
        int cnt = await DbContext
            .Videos
            .Where(x => x.Id == request.Id).
            ExecuteDeleteAsync(cancellationToken);

        return new DeleteVideoResponse(request.Id);
    }
}
