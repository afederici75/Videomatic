﻿namespace Company.Videomatic.Domain;

public abstract class ImportedEntity<TId> : Entity<TId>
{
    public ImportedEntity(string name, string? description)
        : this()
    {
        SetName(name);
        SetDescription(description);
    }

    public ImportedEntity(EntityOrigin origin)
        : this(origin.Name, origin.Description)
    { 
        SetOrigin(origin);
    }
    
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    
    public bool IsStarred { get; private set; } = false;
    public EntityOrigin Origin { get; private set; } = EntityOrigin.Empty;
    public ImageReference Thumbnail { get; private set; } = ImageReference.Empty;
    public ImageReference Picture { get; private set; } = ImageReference.Empty;

    public IEnumerable<string> Tags => _tags;

    public void SetTags(params string[] tags)
    {
        _tags.Clear();

        AddTags(tags);
    }

    public void ClearTags() => _tags.Clear();

    public void AddTags(params string[] tags)
    {
        Guard.Against.Null(tags, nameof(tags));

        foreach (var tag in tags)
        {
            _tags.Add(tag);
        }
    }

    public void RemoveTags(params string[] tags)
    {
        Guard.Against.Null(tags, nameof(tags));

        foreach (var tag in tags)
        {
            _tags.Remove(tag);
        }
    }

    public void SetName(string name) => Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
    public void SetDescription(string? description) => Description = description;
    public void ClearThumbnail() => Thumbnail = ImageReference.Empty;
    public void SetThumbnail(ImageReference thumbnail) => Thumbnail = Guard.Against.Null(thumbnail, nameof(thumbnail));    
    public void ClearPicture() => Picture = ImageReference.Empty;    
    public void SetPicture(ImageReference picture) => Picture = Guard.Against.Null(picture, nameof(picture));
    public void ClearOrigin() => Origin = EntityOrigin.Empty;
    public void SetOrigin(EntityOrigin origin) => Origin = Guard.Against.Null(origin, nameof(origin));
    
    public void ToggleStarred()
    {
        IsStarred = !IsStarred;
    }

    #region Private 
    protected ImportedEntity() : base()
    { }

    readonly HashSet<string> _tags = new();

    #endregion
}