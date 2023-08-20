namespace Infrastructure.YouTube.Extensions;

public static class GenericVideoExtensions
{
    public static GenericVideoDTO ToGenericVideo(this Google.Apis.YouTube.v3.Data.Video video)
    {
        var pict = video.Snippet.Thumbnails.Maxres ?? video.Snippet.Thumbnails.Standard ?? video.Snippet.Thumbnails.High ?? video.Snippet.Thumbnails.Medium ?? video.Snippet.Thumbnails.Default__ ?? new();
        var thumb = video.Snippet.Thumbnails.Default__ ?? video.Snippet.Thumbnails.Medium ?? video.Snippet.Thumbnails.High ?? video.Snippet.Thumbnails.Standard ?? video.Snippet.Thumbnails.Maxres ?? new();

        return new GenericVideoDTO(ProviderId: "YOUTUBE",
                                ProviderItemId: video.Id,
                                ETag: video.ETag,
                                channelId: video.Snippet.ChannelId,
                                channelName: video.Snippet.ChannelTitle,
                                name: video.Snippet.Title,
                                description: video.Snippet.Description,
                                publishedOn: video.Snippet.PublishedAtDateTimeOffset?.UtcDateTime ?? DateTime.UtcNow,
                                thumbnail: new(thumb.Url, Convert.ToInt32(thumb.Height), Convert.ToInt32(thumb.Width)),
                                picture: new(pict.Url, Convert.ToInt32(pict.Height), Convert.ToInt32(pict.Width)),
                                embedHtml: video.Player.EmbedHtml,
                                defaultLanguage: video.Snippet.DefaultLanguage,
                                localizationInfo: new NameAndDescription(video.Snippet.Localized.Title, video.Snippet.Localized.Description),
                                privacyStatus: video.Status.PrivacyStatus,
                                tags: video.Snippet.Tags ?? Array.Empty<string>(),
                                topicCategories: video.TopicDetails?.TopicCategories ?? Array.Empty<string>());
    }
}
