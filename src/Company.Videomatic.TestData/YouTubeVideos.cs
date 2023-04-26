namespace Company.Videomatic.TestData;

public static class YouTubeVideos
{
    public const string RickAstley_NeverGonnaGiveYouUp = "dQw4w9WgXcQ";
    public const string AldousHuxley_DancingShiva = "n1kmKpjk_8E";
    public const string SwamiTadatmananda_WhySoManyGodsInHinduism = "BBd3aHnVnuE";
    public const string HyonGakSunim_WhatIsZen = "BFfb2P5wxC0";

    public static Uri GetUri(string videoId) => new Uri($"https://www.youtube.com/watch?v={videoId}");

    public static class Tips
    {
        public record VideoInfo(string Title, int TransctriptCount, int ThumbnailsCount);

        public static IReadOnlyDictionary<string, VideoInfo> VideosTips = new Dictionary<string, VideoInfo>
        {
            {
                YouTubeVideos.RickAstley_NeverGonnaGiveYouUp,
                new VideoInfo("Rick Astley - Never Gonna Give You Up (Official Music Video)", 1, 5) },

            {
                YouTubeVideos.AldousHuxley_DancingShiva,
                new VideoInfo("Aldous Huxley - The Dancing Shiva", 1, 5) },

            {
                YouTubeVideos.SwamiTadatmananda_WhySoManyGodsInHinduism,
                new VideoInfo("If Reality is NON-DUAL, Why are there so many GODS in Hinduism?", 1, 5) },

            {
                YouTubeVideos.HyonGakSunim_WhatIsZen,
                new VideoInfo("What is ZEN ? - Hyon Gak Sunim", 1, 5) }
        };
    }
}
