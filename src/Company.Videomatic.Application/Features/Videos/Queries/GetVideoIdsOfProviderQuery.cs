namespace Company.Videomatic.Application.Features.Videos.Queries;

public record GetVideoIdsOfProviderQuery(IEnumerable<long> VideoIds) : IRequest<Result<IEnumerable<VideoIdAndProviderVideoIdDTO>>>
{
    public GetVideoIdsOfProviderQuery(IEnumerable<VideoId> VideoIds) : this(VideoIds.Select(v => (long)v)) 
    {
        // TODO: this should not be needed... 
    }
};


internal class GetProviderVideoIdsQueryValidator : AbstractValidator<GetVideoIdsOfProviderQuery>
{
    public GetProviderVideoIdsQueryValidator()
    {
        RuleFor(x => x.VideoIds).NotEmpty();        
    }
}