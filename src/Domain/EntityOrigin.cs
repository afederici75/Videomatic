namespace Domain;

public class EntityOrigin : ValueObject
{
    public static EntityOrigin Empty => new(providerId: string.Empty,
                                            providerItemId: string.Empty,
                                            etag: string.Empty,
                                            channelId: string.Empty,
                                            channelName: string.Empty,
                                            name: string.Empty,
                                            description: string.Empty,
                                            publishedOn: null,
                                            embedHtml: string.Empty,
                                            defaultLanguage: string.Empty,
                                            thumbnail: ImageReference.Empty,
                                            picture: ImageReference.Empty);

    public EntityOrigin(
        string providerId,
        string providerItemId,
        string etag,
        string channelId,
        string channelName,

        string name,
        string? description,

        DateTime? publishedOn,

        string? embedHtml,
        string? defaultLanguage,
        ImageReference thumbnail,
        ImageReference picture
    )
    {
        ProviderId =  providerId;
        ProviderItemId = providerItemId;
        ETag = etag;
        ChannelId = channelId;
        ChannelName = channelName;
        Name = name;
        Description = description;
        PublishedOn = publishedOn;
        EmbedHtml = embedHtml;
        DefaultLanguage = defaultLanguage;
        Thumbnail = thumbnail;
        Picture = picture;
    }

    public string ProviderId { get; private set; } = default!;
    public string ProviderItemId { get; private set; } = default!;
    public string ETag { get; private set; } = default!;
    public string ChannelId { get; private set; } = default!;
    public string ChannelName { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public DateTime? PublishedOn { get; private set; }
    public string? EmbedHtml { get; private set; }
    public string? DefaultLanguage { get; private set; }
    public ImageReference Thumbnail { get; private set; } = default!;
    public ImageReference Picture { get; private set; } = default!;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ProviderId.ToUpper(); // Case insensitive
        yield return ProviderItemId.ToUpper(); // Case insensitive
        yield return ETag.ToUpper(); // Case insensitive
        yield return ChannelId.ToUpper(); // Case insensitive
        yield return ChannelName.ToUpper(); // Case insensitive
        yield return Name.ToUpper(); // Case insensitive
        
        //yield return Description?.ToUpper(); // Case insensitive
        //yield return PublishedOn;
        //yield return EmbedHtml?.ToUpper(); // Case insensitive
        //yield return DefaultLanguage?.ToUpper(); // Case insensitive
        //yield return Thumbnail;
        //yield return Picture;
    }

    #region Private

    private EntityOrigin()
    { }

    #endregion
}
