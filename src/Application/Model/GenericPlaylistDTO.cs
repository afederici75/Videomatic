using SharedKernel.Model;

namespace Application.Model;

public class GenericPlaylistDTO(
    // Inherited
    string providerId,
    string providerItemId,
    string etag,
    string channelId,
    string channelName,
    string name,
    string? description,

    DateTime? publishedOn,

    ImageReference thumbnail,
    ImageReference picture,
    IEnumerable<string>? etags,

    // New
    string? embedHtml,

    string? defaultLanguage,
    NameAndDescription? localizationInfo,

    string privacyStatus,

    int VideoCount) : ImportableDTOBase(
        providerId: providerId, providerItemId: providerItemId, etag: etag, channelId: channelId, channelName: channelName,
        name: name, description: description, publishedOn: publishedOn, thumbnail: thumbnail, picture: picture, tags: etags, 
        defaultLanguage: defaultLanguage, localizationInfo: localizationInfo)
{ 
    public string? EmbedHtml { get; } = embedHtml;
    public string PrivacyStatus { get; } = privacyStatus;
    public int VideoCount { get; } = VideoCount;
}

public static class GenericPlaylistExtensions
{
    public static EntityOrigin ToEntityOrigin(this GenericPlaylistDTO gpl)
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


    public static Playlist ToPlaylist(this GenericPlaylistDTO gpl)
    {
        var res = new Playlist(gpl.Name, gpl.Description);
        res.SetOrigin(gpl.ToEntityOrigin());
        return res;
    }
}