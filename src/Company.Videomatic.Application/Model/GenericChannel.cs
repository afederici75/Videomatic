namespace Company.Videomatic.Application.Model;

public record GenericChannel(
    // Inherited
    string ProviderId,
    string ProviderItemId,
    string ETag,    

    string Name,
    string? Description,

    DateTime? PublishedAt,

    Thumbnail Thumbnail,
    Thumbnail Picture,

    // New
    string? DefaultLanguage,
    NameAndDescription? LocalizationInfo,

    string? Owner,
    DateTime? TimeCreated,

    IEnumerable<string> TopicIds,
    IEnumerable<string> TopicCategories,

    ulong? VideoCount,
    ulong? SuscriberCount,
    ulong? ViewCount); 