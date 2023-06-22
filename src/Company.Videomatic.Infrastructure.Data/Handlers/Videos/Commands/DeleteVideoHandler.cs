namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Commands;

public sealed class DeleteVideoHandler : BaseRequestHandler<DeleteVideoCommand, DeletedResponse>
{
    public DeleteVideoHandler(VideomaticDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public override async Task<DeletedResponse> Handle(DeleteVideoCommand request, CancellationToken cancellationToken = default)
    {
        int cnt = await DbContext
            .Videos
            .Where(x => x.Id == request.Id).
            ExecuteDeleteAsync(cancellationToken);

        return new DeletedResponse(request.Id, cnt > 0);
    }
}
