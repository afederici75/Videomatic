using Domain.Videos;

namespace Application.Model;

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

    ImageReference Thumbnail,
    ImageReference Picture,
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
        => new (providerId: gv.ProviderId,
                providerItemId: gv.ProviderItemId,
                etag: gv.ETag,
                channelId: gv.ChannelId,
                channelName: gv.ChannelName,
                name: gv.Name,
                description: gv.Description,
                publishedOn: gv.PublishedAt,
                embedHtml: gv.EmbedHtml,
                defaultLanguage: gv.DefaultLanguage,
                thumbnail: gv.Thumbnail,
                picture: gv.Picture
                );

    public static Video ToVideo(this GenericVideo gv)
    {
        var res = new Video(gv.Name, gv.Description);
        res.SetOrigin(gv.ToEntityOrigin());
        return res;
    }
}