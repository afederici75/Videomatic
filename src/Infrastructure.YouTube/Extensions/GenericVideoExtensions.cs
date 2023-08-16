namespace Infrastructure.YouTube.Extensions;

public static class GenericVideoExtensions
{
    public static GenericVideo ToGenericVideo(this Google.Apis.YouTube.v3.Data.Video video)
    {
        var pict = video.Snippet.Thumbnails.Maxres ?? video.Snippet.Thumbnails.Standard ?? video.Snippet.Thumbnails.High ?? video.Snippet.Thumbnails.Medium ?? video.Snippet.Thumbnails.Default__ ?? new();
        var thumb = video.Snippet.Thumbnails.Default__ ?? video.Snippet.Thumbnails.Medium ?? video.Snippet.Thumbnails.High ?? video.Snippet.Thumbnails.Standard ?? video.Snippet.Thumbnails.Maxres ?? new();

        return new GenericVideo(ProviderId: "YOUTUBE",
                                ProviderItemId: video.Id,
                                ETag: video.ETag,
                                ChannelId: video.Snippet.ChannelId,
                                ChannelName: video.Snippet.ChannelTitle,
                                Name: video.Snippet.Title,
                                Description: video.Snippet.Description,
                                PublishedAt: video.Snippet.PublishedAtDateTimeOffset?.UtcDateTime ?? DateTime.UtcNow,
                                Thumbnail: new(thumb.Url, Convert.ToInt32(thumb.Height), Convert.ToInt32(thumb.Width)),
                                Picture: new(pict.Url, Convert.ToInt32(pict.Height), Convert.ToInt32(pict.Width)),
                                EmbedHtml: video.Player.EmbedHtml,
                                DefaultLanguage: video.Snippet.DefaultLanguage,
                                LocalizationInfo: new NameAndDescription(video.Snippet.Localized.Title, video.Snippet.Localized.Description),
                                PrivacyStatus: video.Status.PrivacyStatus,
                                Tags: video.Snippet.Tags ?? Array.Empty<string>());
    }
}
