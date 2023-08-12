namespace Company.Videomatic.Application.Model;

public abstract record GenericImportable(
    string ProviderId,
    string ProviderItemId,
    string ETag,

    string ChannelId,
    string ChannelName,

    string Name,
    string? Description,

    DateTime? PublishedAt,
    Thumbnail Picture,
    Thumbnail Thumbnail,
    
    IEnumerable<string>? Tags);
