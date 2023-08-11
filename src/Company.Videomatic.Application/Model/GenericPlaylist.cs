namespace Company.Videomatic.Application.Model;

public record GenericPlaylist(
    // Inherited
    string Id,
    string ETag,
    string ChannelId,

    string Name,
    string? Description,

    DateTime? PublishedAt,

    Thumbnail Thumbnail,
    Thumbnail Picture,

    // New
    string? EmbedHtml,

    string? DefaultLanguage,
    NameAndDescription? LocalizationInfo,

    string PrivacyStatus,
    int VideoCount) : GenericImportable(Id, ETag, Name, Description, PublishedAt, Thumbnail, Picture);
