namespace Company.Videomatic.Application.Features.Videos.GetVideos;

public class GetVideosDTOQueryHandler : IRequestHandler<GetVideosDTOQuery, QueryResponse<VideoDTO>>
{
    readonly IReadRepositoryBase<Video> _repository;

    public GetVideosDTOQueryHandler(IReadRepositoryBase<Video> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<QueryResponse<VideoDTO>> Handle(GetVideosDTOQuery request, CancellationToken cancellationToken)
    {
        var query = new GetVideosSpecification(
            take: request.Take,
            titlePrefix: request.TitlePrefix,
            descriptionPrefix: request.DescriptionPrefix,
            providerIdPrefix: request.ProviderIdPrefix,
            videoUrlPrefix: request.VideoUrlPrefix,
            providerVideoIdPrefix: request.ProviderVideoIdPrefix,
            skip: request.Skip,
            includes: request.Includes,
            orderBy: request.OrderBy);

        var videos = await _repository.ListAsync(query, cancellationToken);

        // TODO: Use AutoMapper
        var videosDTO = videos.Select(v => new VideoDTO(
            v.Id,
            v.Title ?? "NA",
            v.Description ?? "NA",
            v.ProviderId,
            v.Description,
            v.Artifacts,
            v.Thumbnails,
            v.Transcripts))
            .ToArray();

        return new QueryResponse<VideoDTO>(videosDTO);
    }
}