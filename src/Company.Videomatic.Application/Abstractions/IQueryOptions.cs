namespace Company.Videomatic.Application.Abstractions;

public interface IQueryOptions
{
    string? SearchText { get; }
    long[]? Ids { get; }
    int? Page { get; }
    int? PageSize { get; init; }
    string? OrderBy { get; init; }
}
