namespace Company.Videomatic.Application.Model;

public record GenericVideo(
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
    NameAndDescription LocalizationInfo,
    string PrivacyStatus,
    IEnumerable<string> Tags) : GenericImportable(Id, ETag, Name, Description, PublishedAt, PictureUrl, ThumbnailUrl);