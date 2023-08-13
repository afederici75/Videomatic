namespace Company.Videomatic.Domain;

public record EntityOrigin(
    string ProviderId,
    string ProviderItemId,
    string ETag,
    string ChannelId,
    string ChannelName,

    string Name,
    string? Description,

    DateTime? PublishedOn,

    //Thumbnail Thumbnail,
    //Thumbnail Picture,

    string? EmbedHtml,
    string? DefaultLanguage)
{ 
    public static EntityOrigin Empty => new(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, null, /*Thumbnail.Empty, Thumbnail.Empty,*/ string.Empty, string.Empty);
};
