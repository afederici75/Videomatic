using Company.Videomatic.Domain.Extensions;

namespace Company.Videomatic.Domain.Specifications.Videos;

public class VideosBySearchText : Specification<Video>//, IPaginatedSpecification<Video>
{
    public static readonly IReadOnlyDictionary<string, Expression<Func<Video, object?>>> SupportedOrderBys = new Dictionary<string, Expression<Func<Video, object?>>>(StringComparer.OrdinalIgnoreCase)
    {
        { nameof(Video.Id), _ => _.Id },
        { nameof(Video.Name), _ => _.Name },
        { nameof(Video.Description), _ => _.Description },
        { "PlaylistCount", _ => _.Playlists.Count },
        { "TagCount", _ => _.Tags.Count },
        { "ThumbnailsCount", _ => _.Thumbnails.Count },
    };

    public VideosBySearchText(string? searchText = default,
                              long[]? playlistIds = default,
                              string? orderBy = default)
    {
        // searchText is included in Name and Description
        if (!string.IsNullOrWhiteSpace(searchText))
        {
            Query.Where(v =>
                v.Name.Contains(searchText) ||
                v.Description!.Contains(searchText));
        }

        // Video with references to any of the playlistIds
        if (playlistIds != null && playlistIds.Any())
        {
            Query.Where(v => v.Playlists.Any(pv => playlistIds.Contains(pv.PlaylistId)));
        }

        // OrderBy
        Query.OrderByText(orderBy, SupportedOrderBys);
    }
}