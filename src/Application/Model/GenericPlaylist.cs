using SharedKernel.Model;

namespace Application.Model;

public record GenericPlaylist(
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
    NameAndDescription? LocalizationInfo,

    string PrivacyStatus,

    int VideoCount) : GenericImportable(
        ProviderId: ProviderId, ProviderItemId: ProviderItemId, ETag: ETag, ChannelId: ChannelId, ChannelName: ChannelName, 
        Name: Name, Description: Description, PublishedAt: PublishedAt, Thumbnail: Thumbnail, Picture: Picture, Tags: Tags);

public static class GenericPlaylistExtensions
{
    public static EntityOrigin ToEntityOrigin(this GenericPlaylist gpl)
        => new(providerId: gpl.ProviderId,
               providerItemId: gpl.ProviderItemId,
               etag: gpl.ETag,
               channelId: gpl.ChannelId,
               channelName: gpl.ChannelName,
               name: gpl.Name,
               description: gpl.Description,
               publishedOn: gpl.PublishedAt,
               embedHtml: gpl.EmbedHtml,
               defaultLanguage: gpl.DefaultLanguage,
               thumbnail: gpl.Thumbnail,
               picture: gpl.Picture);


    public static Playlist ToPlaylist(this GenericPlaylist gpl)
    {
        var res = new Playlist(gpl.Name, gpl.Description);
        res.SetOrigin(gpl.ToEntityOrigin());
        return res;
    }
}