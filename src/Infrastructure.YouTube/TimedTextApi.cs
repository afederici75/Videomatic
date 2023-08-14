using System.Text.Json.Serialization;

namespace Infrastructure.YouTube;

public class TimedTextInformation
{
    [JsonPropertyName("playerCaptionsTracklistRenderer")]
    public Playercaptionstracklistrenderer PlayerCaptionsTracklistRenderer { get; set; } = default!;
}

public class Playercaptionstracklistrenderer
{
    [JsonPropertyName("captionTracks")]
    public Captiontrack[] CaptionTracks { get; set; } = default!;
}

public class Captiontrack
{
    [JsonPropertyName("baseUrl")]
    public string BaseUrl { get; set; } = default!;

    [JsonPropertyName("name")]
    public Name Name { get; set; } = default!;

    [JsonPropertyName("vssId")]
    public string VssId { get; set; } = default!;

    [JsonPropertyName("languageCode")]
    public string LanguageCode { get; set; } = default!;

    [JsonPropertyName("kind")]
    public bool IsTranslatable { get; set; } = default!;
}

public class Name
{
    [JsonPropertyName("simpleText")]
    public string SimpleText { get; set; } = default!;
}
