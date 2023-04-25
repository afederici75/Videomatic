namespace Company.Videomatic.Application.Features.Videos.Queries.GetVideos;

public enum SortOrder
{
    Asc,
    Desc
}

public record SortOptions(string PropertyName, SortOrder Order = SortOrder.Asc);
