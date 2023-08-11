
public class TimedTextInformation
{
    public Playercaptionstracklistrenderer playerCaptionsTracklistRenderer { get; set; }
}

public class Playercaptionstracklistrenderer
{
    public Captiontrack[] captionTracks { get; set; }
}

public class Captiontrack
{
    public string baseUrl { get; set; }
    public Name name { get; set; }
    public string vssId { get; set; }
    public string languageCode { get; set; }
    public bool isTranslatable { get; set; }
}

public class Name
{
    public string simpleText { get; set; }
}
