namespace Company.Videomatic.Application.Features.Videos.Queries;

public record GetProviderVideoIdsQuery(IEnumerable<long> VideoIds) : IRequest<Result<IReadOnlyDictionary<long, string>>>
{
    public GetProviderVideoIdsQuery(IEnumerable<VideoId> VideoIds) : this(VideoIds.Select(v => (long)v)) 
    {
        // TODO: this should not be needed... 
    }
};


internal class GetProviderVideoIdsQueryValidator : AbstractValidator<GetProviderVideoIdsQuery>
{
    public GetProviderVideoIdsQueryValidator()
    {
        RuleFor(x => x.VideoIds).NotEmpty();        
    }
}