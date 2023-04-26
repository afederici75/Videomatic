namespace Company.Videomatic.Domain.Tests;

public static class YouTubeVideos
{
    public const string RickAstley_NeverGonnaGiveYouUp = "dQw4w9WgXcQ";
    public const string AldousHuxley_DancingShiva = "n1kmKpjk_8E";
    public const string SwamiTadatmananda_WhySoManyGodsInHinduism = "BBd3aHnVnuE";
    public const string HyonGakSunim_WhatIsZen = "BFfb2P5wxC0";

    public static Uri GetUri(string videoId) => new Uri($"https://www.youtube.com/watch?v={videoId}");
}
