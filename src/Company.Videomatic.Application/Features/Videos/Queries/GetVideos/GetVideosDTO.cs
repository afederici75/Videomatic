namespace Company.Videomatic.Application.Features.Videos.Queries.GetVideos;

public record GetVideosDTO(
    int Id, 
    string ProviderId, 
    string VideoUrl, 
    string? Title, 
    string? Description, 
    IEnumerable<Artifact>? Artifacts, 
    IEnumerable<Thumbnail>? Thumbnails, 
    IEnumerable<Transcript>? Transcripts);