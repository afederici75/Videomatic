namespace Company.Videomatic.Infrastructure.Data.Handlers.Videos.Commands;

public sealed class CreateVideoHandler : IRequestHandler<CreateVideoCommand, CreateVideoResponse>
{
    private readonly IRepository<Video> _repository;
    private readonly IMapper _mapper;

    public CreateVideoHandler(IRepository<Video> repository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<CreateVideoResponse> Handle(CreateVideoCommand request, CancellationToken cancellationToken = default)
    {
        Video newVideo = _mapper.Map<CreateVideoCommand, Video>(request);

        var entry = await _repository.AddAsync(newVideo);

        return new CreateVideoResponse(Id: entry.Id);
    }
}
