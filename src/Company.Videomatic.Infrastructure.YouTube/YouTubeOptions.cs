namespace Company.Videomatic.Infrastructure.YouTube;

public class YouTubeOptions
{
    public required string ApiKey { get; set; }
    public required string ApplicationName { get; set; }
    public required string ChannelName { get; set; }
    
    public required string ClientId { get; set; }
    public required string ClientSecret { get; set; }    
}
