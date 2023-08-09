namespace Company.Videomatic.Application.Model;

public record GenericVideo(
    // Inherited
    string Id,
    string ETag,
    string ChannelId,

    string Name,
    string? Description,

    DateTime? PublishedAt,

    string? ThumbnailUrl,
    string? PictureUrl,

    // New
    string? EmbedHtml,
    string? DefaultLanguage,
    NameAndDescription LocalizationInfo,
    string PrivacyStatus) : GenericImportable(Id, ETag, Name, Description, PublishedAt, ThumbnailUrl, PictureUrl);