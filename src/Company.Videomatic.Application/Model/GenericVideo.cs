namespace Company.Videomatic.Application.Model;

public record GenericVideo(
    // Inherited
    string ProviderId,
    string ProviderItemId,
    string ETag,
    string ChannelId,
    string ChannelName,

    string Name,
    string? Description,

    DateTime? PublishedAt,

    Thumbnail Thumbnail,
    Thumbnail Picture,
    IEnumerable<string>? Tags,

    // New
    string? EmbedHtml,
    string? DefaultLanguage,
    NameAndDescription LocalizationInfo,
    string PrivacyStatus) : GenericImportable(ProviderId: ProviderId, ProviderItemId: ProviderItemId, ETag: ETag, ChannelId: ChannelId, ChannelName: ChannelName,
                                                  Name: Name, Description: Description, PublishedAt: PublishedAt, Thumbnail: Thumbnail, Picture: Picture, Tags: Tags);


public static class GenericVideoExtensions
{
    public static EntityOrigin ToEntityOrigin(this GenericVideo gv)
        => new (ProviderId: gv.ProviderId,
                ProviderItemId: gv.ProviderItemId,
                ETag: gv.ETag,
                ChannelId: gv.ChannelId,
                ChannelName: gv.ChannelName,
                Name: gv.Name,
                Description: gv.Description,
                PublishedOn: gv.PublishedAt,
                Thumbnail: gv.Thumbnail,
                Picture: gv.Picture,
                EmbedHtml: gv.EmbedHtml,
                DefaultLanguage: gv.DefaultLanguage);

    public static Video ToVideo(this GenericVideo gv)
    {
        var res = new Video(gv.Name, gv.Description);
        res.SetOrigin(gv.ToEntityOrigin());
        return res;
    }
}