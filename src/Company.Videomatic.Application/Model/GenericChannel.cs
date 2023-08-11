namespace Company.Videomatic.Application.Model;

public record GenericChannel(
    // Inherited
    string Id,
    string ETag,

    string Name,
    string? Description,

    DateTime? PublishedAt,

    string ThumbnailUrl,
    string PictureUrl,

    // New
    string? DefaultLanguage,
    NameAndDescription? LocalizationInfo,

    string? Owner,
    DateTime? TimeCreated,

    IEnumerable<string> TopicIds,
    IEnumerable<string> TopicCategories,

    ulong? VideoCount,
    ulong? SuscriberCount,
    ulong? ViewCount) : GenericImportable(Id, ETag, Name, Description, PublishedAt, ThumbnailUrl, PictureUrl);
