namespace Company.Videomatic.Application.Model;

public record GenericChannel(
    string Id,
    string ETag,

    string Name,
    string? Description,

    DateTime PublishedAt,

    string? ThumbnailUrl,
    string? PictureUrl,

    string? DefaultLanguage,
    LocalizationInfo? LocalizationInfo,

    ContentOwnerDetail? ContentOwnerDetail,
    GenericChannelTopics? Topics,
    GenericChannelStatistcs? Statistics);


public record GenericChannelTopics(
    IEnumerable<string> TopicIds,
    IEnumerable<string> TopicCategories);

public record LocalizationInfo(
    string Name,
    string? Description);

public record ContentOwnerDetail(
    string ContentOwner,
    DateTime? TimeLinked);

public record GenericChannelStatistcs(
    ulong? VideoCount,
    ulong? SuscriberCount,
    ulong? ViewCount);

public record GenericPlaylist(
    string Id,
    string ETag,
    string ChannelId,

    string Name,
    string? Description,

    DateTime PublishedAt,

    string? ThumbnailUrl,
    string? PictureUrl,
    string? EmbedHtml,

    string? DefaultLanguage,
    LocalizationInfo LocalizationInfo,

    string PrivacyStatus,

    int VideoCount);

public record GenericVideo(
    string Id,
    string ETag,
    string ChannelId,

    string Name,
    string? Description,

    DateTime PublishedAt,

    string? ThumbnailUrl,
    string? PictureUrl,
    string? EmbedHtml,

    string? DefaultLanguage,
    LocalizationInfo LocalizationInfo,
    string PrivacyStatus);