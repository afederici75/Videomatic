namespace Application.Model;

public static class GenericVideoExtensions
{
    public static EntityOrigin ToEntityOrigin(this GenericVideoDTO gv)
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
                picture: gv.Picture);

    public static Video ToVideo(this GenericVideoDTO gv)
    {
        // TODO: this is duplicated code from GenericPlaylistExtensions.cs
        var res = new Video(gv.Name, gv.Description);
        res.SetOrigin(gv.ToEntityOrigin());
        
        if (gv.Tags != null)
            res.SetTags(gv.Tags);

        if (gv.TopicCategories != null)
            res.SetTopicCategories(gv.TopicCategories);

        return res;
    }
}