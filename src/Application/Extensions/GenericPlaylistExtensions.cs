namespace Application.Model;

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
        if (gpl.Tags != null)
            res.SetTags(gpl.Tags);
                
        return res;
    }
}