using SharedKernel.Model;

namespace Application.Features.Videos;

public class VideoDTO(
    int id = 0,
    string location = "",
    string name = "",
    string? description = null,
    IEnumerable<string>? tags = null,
    ImageReference? thumbnail = null,
    ImageReference? picture = null,
    // 
    int artifactCount = 0,
    int transcriptCount = 0,
    int tagCount = 0,
    //
    string provider = "",
    string providerVideoId = "",
    DateTime videoPublishedAt = default,
    string videoOwnerChannelTitle = "",
    string videoOwnerChannelId = ""
    )
{ 
    public int Id { get; } = id;
    public string Location { get; } = location;
    public string Name { get; } = name;
    public string? Description { get; } = description;
    public IEnumerable<string>? Tags { get; } = tags;
    public ImageReference? Thumbnail { get; } = thumbnail;
    public ImageReference? Picture { get; } = picture;
    //
    public int ArtifactCount { get; } = artifactCount;
    public int TranscriptCount { get; } = transcriptCount;
    public int TagCount { get; } = tagCount;
    //
    public string Provider { get; } = provider;
    public string ProviderVideoId { get; } = providerVideoId;
        
    public DateTime VideoPublishedAt { get; } = videoPublishedAt;
    public string VideoOwnerChannelTitle { get; } = videoOwnerChannelTitle;
    public string VideoOwnerChannelId { get; } = videoOwnerChannelId;            
}