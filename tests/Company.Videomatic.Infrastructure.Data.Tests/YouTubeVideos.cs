namespace Company.Videomatic.Infrastructure.Data;

public static class YouTubeVideos
{
    public const string RickAstley_NeverGonnaGiveYouUp = "dQw4w9WgXcQ";
    public const string AldousHuxley_DancingShiva = "n1kmKpjk_8E";
    public const string SwamiTadatmananda_WhySoManyGodsInHinduism = "BBd3aHnVnuE";
    public const string HyonGakSunim_WhatIsZen = "BFfb2P5wxC0";

    public record VideoHint(string ProviderVideoId, string VideoUrl, string Title, int TransctriptCount, int ThumbnailsCount);
    
    readonly static List<VideoHint> _videoHints;
    static YouTubeVideos()
    {
        _videoHints = new List<VideoHint>()
            {
                new VideoHint(
                        YouTubeVideos.RickAstley_NeverGonnaGiveYouUp,
                        GetUrl(YouTubeVideos.RickAstley_NeverGonnaGiveYouUp),
                        "Rick Astley - Never Gonna Give You Up (Official Music Video)", 1, 5),

                    new VideoHint(
                        YouTubeVideos.AldousHuxley_DancingShiva,
                        GetUrl(YouTubeVideos.AldousHuxley_DancingShiva),
                        "Aldous Huxley - The Dancing Shiva", 1, 5),

                    new VideoHint(
                        YouTubeVideos.SwamiTadatmananda_WhySoManyGodsInHinduism,
                        GetUrl(YouTubeVideos.SwamiTadatmananda_WhySoManyGodsInHinduism),
                        "If Reality is NON-DUAL, Why are there so many GODS in Hinduism?", 1, 5),

                    new VideoHint(
                        YouTubeVideos.HyonGakSunim_WhatIsZen,
                        GetUrl(YouTubeVideos.HyonGakSunim_WhatIsZen),
                        "What is ZEN ? - Hyon Gak Sunim", 1, 5)
            };
    }

    public static string[] GetVideoIds() => _videoHints.Select(v => v.ProviderVideoId).ToArray();
    public static string GetUrl(string videoId) => GetUri(videoId).ToString();
    public static Uri GetUri(string videoId) => new ($"https://www.youtube.com/watch?v={videoId}");

    public static VideoHint GetInfoByVideoId(string videoId)
        => _videoHints.First(v => v.ProviderVideoId.Equals(videoId, StringComparison.OrdinalIgnoreCase));

    public static VideoHint GetInfoByUri(Uri uri)
        => GetInfoByUrl(uri.ToString());

    public static VideoHint GetInfoByUrl(string url)
        => _videoHints.First(v => v.VideoUrl.Equals(url, StringComparison.OrdinalIgnoreCase));

    public static int HintsCount => _videoHints.Count;

    public static IEnumerable<VideoHint> GetHints() => _videoHints.ToArray();
}