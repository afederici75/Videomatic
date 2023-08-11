namespace Company.Videomatic.Application.Model;

public abstract record GenericImportable(
    string Id,
    string ETag,
    string Name,
    string? Description,
    DateTime? PublishedAt,
    Thumbnail Picture,
    Thumbnail Thumbnail);
