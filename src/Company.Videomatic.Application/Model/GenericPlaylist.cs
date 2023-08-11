namespace Company.Videomatic.Application.Model;

public record GenericPlaylist(
    // Inherited
    string Id,
    string ETag,
    string ChannelId,

    string Name,
    string? Description,

    DateTime? PublishedAt,

    string ThumbnailUrl,
    string PictureUrl,

    // New
    string? EmbedHtml,

    string? DefaultLanguage,
    NameAndDescription? LocalizationInfo,

    string PrivacyStatus,
    int VideoCount) : GenericImportable(Id, ETag, Name, Description, PublishedAt, ThumbnailUrl, PictureUrl);
