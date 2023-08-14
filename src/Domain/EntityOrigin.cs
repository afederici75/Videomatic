namespace Domain;

public record EntityOrigin(
    string ProviderId,
    string ProviderItemId,
    string ETag,
    string ChannelId,
    string ChannelName,

    string Name,
    string? Description,

    DateTime? PublishedOn,
    
    string? EmbedHtml,
    string? DefaultLanguage,
    ImageReference Thumbnail
    //Thumbnail Picture,
)
{ 
    private EntityOrigin() : this(string.Empty,
                                  string.Empty,
                                  string.Empty,
                                  string.Empty,
                                  string.Empty,
                                  string.Empty,
                                  string.Empty,
                                  null,
                                  string.Empty,
                                  string.Empty,
                                  ImageReference.Empty)
    { }

    public static EntityOrigin Empty => new(ProviderId: string.Empty,
                                            ProviderItemId: string.Empty,
                                            ETag: string.Empty,
                                            ChannelId: string.Empty,
                                            ChannelName: string.Empty,
                                            Name: string.Empty,
                                            Description: string.Empty,
                                            PublishedOn: null,
                                            EmbedHtml: string.Empty,
                                            DefaultLanguage: string.Empty,
                                            Thumbnail: ImageReference.Empty);
};
