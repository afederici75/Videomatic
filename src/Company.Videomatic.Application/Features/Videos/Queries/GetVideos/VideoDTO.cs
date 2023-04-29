﻿namespace Company.Videomatic.Application.Features.Videos.Queries;

public record VideoDTO(
    int Id, 
    string ProviderId, 
    string VideoUrl, 
    string? Title, 
    string? Description, 
    IEnumerable<Artifact>? Artifacts, 
    IEnumerable<Thumbnail>? Thumbnails, 
    IEnumerable<Transcript>? Transcripts);

// TODO: Should have a DTO for each entity