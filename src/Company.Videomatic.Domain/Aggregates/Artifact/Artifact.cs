﻿using Company.Videomatic.Domain.Aggregates.Video;

namespace Company.Videomatic.Domain.Aggregates.Artifact;

public class Artifact : Entity<ArtifactId>, IAggregateRoot
{
    public Artifact(VideoId videoId, string name, string type, string? text = null)
    {
        VideoId = videoId;
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
        Type = Guard.Against.NullOrWhiteSpace(type);
        Text = text;
    }

    public VideoId VideoId { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public string Type { get; private set; } = default!;
    public string? Text { get; private set; }
    
    #region Private

    private Artifact()
    { }

    int IEntity.Id => this.Id;

    #endregion
}

