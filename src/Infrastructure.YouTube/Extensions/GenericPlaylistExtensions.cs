namespace Infrastructure.YouTube.Extensions;

public static class GenericPlaylistExtensions
{

    public static GenericPlaylist ToGenericPlaylist(this Google.Apis.YouTube.v3.Data.Playlist playlist)
    {
        var thumbs = playlist.Snippet!.Thumbnails!;

        var thub = thumbs.Default__ ?? thumbs.Medium ?? thumbs.High ?? thumbs.Standard ?? thumbs.Maxres ?? new Google.Apis.YouTube.v3.Data.Thumbnail();//", -1, -1);
        var pict = thumbs.Maxres ?? thumbs.Standard ?? thumbs.High ?? thumbs.Medium ?? thumbs.Default__ ?? new Google.Apis.YouTube.v3.Data.Thumbnail();//"", -1, -1);

        return new GenericPlaylist(
            ProviderId: "YOUTUBE",
            ProviderItemId: playlist.Id,
            ETag: playlist.ETag,
            ChannelId: playlist.Snippet.ChannelId,
            ChannelName: playlist.Snippet.ChannelTitle,
            Name: playlist.Snippet.Title,
            Description: playlist.Snippet.Description,
            PublishedAt: playlist.Snippet.PublishedAtDateTimeOffset?.UtcDateTime ?? DateTime.UtcNow,
            Thumbnail: new(thub.Url, Convert.ToInt32(thub.Height), Convert.ToInt32(thub.Width)),
            Picture: new(pict.Url, Convert.ToInt32(pict.Height), Convert.ToInt32(pict.Width)),
            Tags: playlist.Snippet.Tags,
            EmbedHtml: playlist.Player.EmbedHtml,
            DefaultLanguage: playlist.Snippet.DefaultLanguage,
            LocalizationInfo: new NameAndDescription(playlist.Snippet.Localized?.Title ?? "??", playlist.Snippet.Localized?.Description),
            PrivacyStatus: playlist.Status.PrivacyStatus,
            VideoCount: Convert.ToInt32(playlist.ContentDetails.ItemCount));
    }
}
