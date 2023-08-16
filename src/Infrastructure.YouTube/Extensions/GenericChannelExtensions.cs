using Google.Apis.YouTube.v3.Data;

namespace Infrastructure.YouTube.Extensions;

public static class GenericChannelExtensions
{
    public static GenericChannel ToGenericChannel(this Channel channel)
    {
        var thumbs = channel.Snippet!.Thumbnails!;

        var thumb = thumbs.Default__ ?? thumbs.Medium ?? thumbs.High ?? thumbs.Standard ?? thumbs.Maxres ?? new Thumbnail();//", -1, -1);
        var pict = thumbs.Maxres ?? thumbs.Standard ?? thumbs.High ?? thumbs.Medium ?? thumbs.Default__ ?? new Thumbnail();//"", -1, -1);

        NameAndDescription? locInfo = null;
        if (channel.Snippet.Localized != null)
            locInfo = new(channel.Snippet.Localized.Title, channel.Snippet.Localized.Description);

        return new GenericChannel(
            ProviderId: "YOUTUBE",
            ProviderItemId: channel.Id,
            ETag: channel.ETag,
            Name: channel.Snippet.Title,
            Description: channel.Snippet.Description,
            PublishedAt: channel.Snippet.PublishedAtDateTimeOffset?.UtcDateTime,
            Thumbnail: new(thumb.Url, Convert.ToInt32(thumb.Height), Convert.ToInt32(thumb.Width)),
            Picture: new(pict.Url, Convert.ToInt32(pict.Height), Convert.ToInt32(pict.Width)),
            DefaultLanguage: channel.Snippet.DefaultLanguage,
            LocalizationInfo: locInfo,
            Owner: channel.ContentOwnerDetails?.ContentOwner,
            TimeCreated: channel.ContentOwnerDetails?.TimeLinkedDateTimeOffset?.UtcDateTime,
            TopicCategories: channel.TopicDetails?.TopicCategories ?? Enumerable.Empty<string>(),
            TopicIds: channel.TopicDetails?.TopicIds ?? Enumerable.Empty<string>(),
            VideoCount: channel.Statistics?.VideoCount,
            SuscriberCount: channel.Statistics?.SubscriberCount,
            ViewCount: channel.Statistics?.ViewCount);
    }
}