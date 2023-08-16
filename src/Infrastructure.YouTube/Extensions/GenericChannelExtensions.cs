using Google.Apis.YouTube.v3.Data;
using System.Diagnostics.Tracing;

namespace Infrastructure.YouTube.Extensions;

public static class GenericChannelExtensions
{
    public static GenericChannelDTO ToGenericChannel(this Channel channel)
    {
        // https://developers.google.com/youtube/v3/docs/channels
        var thumbs = channel.Snippet!.Thumbnails!;

        var thumb = thumbs.Default__ ?? thumbs.Medium ?? thumbs.High ?? thumbs.Standard ?? thumbs.Maxres ?? new Thumbnail();//", -1, -1);
        var pict = thumbs.Maxres ?? thumbs.Standard ?? thumbs.High ?? thumbs.Medium ?? thumbs.Default__ ?? new Thumbnail();//"", -1, -1);

        NameAndDescription? locInfo = null;
        if (channel.Snippet.Localized != null)
            locInfo = new(channel.Snippet.Localized.Title, channel.Snippet.Localized.Description);

        return new GenericChannelDTO(
            providerId: "YOUTUBE",
            providerItemId: channel.Id,
            etag: channel.ETag,
            name: channel.Snippet.Title,
            description: channel.Snippet.Description,
            publishedOn: channel.Snippet.PublishedAtDateTimeOffset?.UtcDateTime,
            thumbnail: new(thumb.Url, Convert.ToInt32(thumb.Height), Convert.ToInt32(thumb.Width)),
            picture: new(pict.Url, Convert.ToInt32(pict.Height), Convert.ToInt32(pict.Width)),
            tags: (channel.TopicDetails?.TopicIds ?? Array.Empty<string>())
                   .Concat(channel.TopicDetails?.TopicCategories ?? Array.Empty<string>()),
            defaultLanguage: channel.Snippet.DefaultLanguage,
            localizationInfo: locInfo,
            owner: channel.ContentOwnerDetails?.ContentOwner,
            timeCreated: channel.ContentOwnerDetails?.TimeLinkedDateTimeOffset?.UtcDateTime,
            topicCategories: channel.TopicDetails?.TopicCategories ?? Enumerable.Empty<string>(),
            topicIds: channel.TopicDetails?.TopicIds ?? Enumerable.Empty<string>(),
            videoCount: channel.Statistics?.VideoCount,
            suscriberCount: channel.Statistics?.SubscriberCount,
            viewCount: channel.Statistics?.ViewCount);
    }
}