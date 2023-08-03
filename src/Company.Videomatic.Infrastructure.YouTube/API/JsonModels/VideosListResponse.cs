#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Company.Videomatic.Infrastructure.YouTube.API.JsonPasteSpecial;

// API: https://developers.google.com/youtube/v3/docs/videos/list
// Example https://www.googleapis.com/youtube/v3/videos?part=id,snippet&id=4Y4YSpF6d6w,tWZQPCU4LJI

// TODO: fix the warnings and possibly use directly the Google API nuget

public class VideosListResponse
{
    public string kind { get; set; }
    public string etag { get; set; }
    public Item[] items { get; set; }
    public Pageinfo pageInfo { get; set; }

    public class Pageinfo
    {
        public int totalResults { get; set; }
        public int resultsPerPage { get; set; }
    }

    public class Item
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public string id { get; set; }
        public Snippet snippet { get; set; }
    }

    public class Snippet
    {
        public DateTime publishedAt { get; set; }
        public string channelId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Thumbnails thumbnails { get; set; }
        public string channelTitle { get; set; }
        public string[] tags { get; set; }
        public string categoryId { get; set; }
        public string liveBroadcastContent { get; set; }
        public Localized localized { get; set; }
        public string defaultAudioLanguage { get; set; }
    }

    public class Thumbnails
    {
        public Thumbnail @default { get; set; }
        public Thumbnail medium { get; set; }
        public Thumbnail high { get; set; }
        public Thumbnail standard { get; set; }
        public Thumbnail maxres { get; set; }
    }

    public class Thumbnail
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Localized
    {
        public string title { get; set; }
        public string description { get; set; }
    }
}


