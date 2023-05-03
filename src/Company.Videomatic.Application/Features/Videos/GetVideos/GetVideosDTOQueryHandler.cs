namespace Company.Videomatic.Application.Features.Videos.GetVideos;

public class GetVideosDTOQueryHandler : IRequestHandler<GetVideosDTOQuery, QueryResponse<VideoDTO>>
{
    readonly IReadOnlyRepository<Video> _repository;

    public GetVideosDTOQueryHandler(IReadOnlyRepository<Video> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<QueryResponse<VideoDTO>> Handle(GetVideosDTOQuery request, CancellationToken cancellationToken)
    {
        var query = new GetVideosSpecification(
            take: request.Take ?? 10,
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
            Id: v.Id,
            ProviderId: v.ProviderId,
            ProviderVideoId: v.ProviderVideoId,
            VideoUrl: v.VideoUrl,
            Title:v.Title ?? "NA",
            Description: v.Description ?? "NA",            
            Artifacts: v.Artifacts,
            Thumbnails: v.Thumbnails,
            Transcripts: v.Transcripts))
            .ToArray();

        return new QueryResponse<VideoDTO>(videosDTO);
    }
}