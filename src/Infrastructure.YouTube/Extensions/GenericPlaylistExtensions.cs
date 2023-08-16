namespace Infrastructure.YouTube.Extensions;

public static class GenericPlaylistExtensions
{

    public static GenericPlaylistDTO ToGenericPlaylist(this Google.Apis.YouTube.v3.Data.Playlist playlist)
    {
        var thumbs = playlist.Snippet!.Thumbnails!;

        var thub = thumbs.Default__ ?? thumbs.Medium ?? thumbs.High ?? thumbs.Standard ?? thumbs.Maxres ?? new Google.Apis.YouTube.v3.Data.Thumbnail();//", -1, -1);
        var pict = thumbs.Maxres ?? thumbs.Standard ?? thumbs.High ?? thumbs.Medium ?? thumbs.Default__ ?? new Google.Apis.YouTube.v3.Data.Thumbnail();//"", -1, -1);

        return new GenericPlaylistDTO(
            providerId: "YOUTUBE",
            providerItemId: playlist.Id,
            etag: playlist.ETag,
            channelId: playlist.Snippet.ChannelId,
            channelName: playlist.Snippet.ChannelTitle,
            name: playlist.Snippet.Title,
            description: playlist.Snippet.Description,
            publishedOn: playlist.Snippet.PublishedAtDateTimeOffset?.UtcDateTime ?? DateTime.UtcNow,
            thumbnail: new(thub.Url, Convert.ToInt32(thub.Height), Convert.ToInt32(thub.Width)),
            picture: new(pict.Url, Convert.ToInt32(pict.Height), Convert.ToInt32(pict.Width)),
            etags: playlist.Snippet.Tags,
            embedHtml: playlist.Player.EmbedHtml,
            defaultLanguage: playlist.Snippet.DefaultLanguage,
            localizationInfo: new NameAndDescription(playlist.Snippet.Localized?.Title ?? "??", playlist.Snippet.Localized?.Description),
            privacyStatus: playlist.Status.PrivacyStatus,
            VideoCount: Convert.ToInt32(playlist.ContentDetails.ItemCount));
    }
}
