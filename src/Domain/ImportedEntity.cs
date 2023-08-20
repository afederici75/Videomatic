namespace Domain;

public abstract class ImportedEntity : TrackedEntity
{
    public ImportedEntity(string name, string? description)
        : base()
    {
        SetName(name);
        SetDescription(description);
    }

    public ImportedEntity(EntityOrigin origin)
        : this(origin.Name, origin.Description)
    { 
        SetOrigin(origin);

        SetThumbnail(origin.Thumbnail);
        SetPicture(origin.Picture);
    }
    
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    
    public bool IsStarred { get; private set; } = false;
    public EntityOrigin Origin { get; private set; } = EntityOrigin.Empty;
    public ImageReference? Thumbnail { get; private set; }
    public ImageReference? Picture { get; private set; }

    public IEnumerable<string> Tags => _tags;

    public IEnumerable<string> TopicCategories => _topicCategories;

    public void SetTopicCategories(IEnumerable<string> topicCategories)
    {
        _topicCategories.Clear();

        foreach (var x in topicCategories)
        {
            _topicCategories.Add(x);
        }
    }

    public void SetTags(IEnumerable<string> tags)
    {
        _tags.Clear();

        foreach (var x in tags)
        {
            _tags.Add(x);
        }
    }

    public void ClearTags() => _tags.Clear();
    
    public void SetName(string name) => Name = name;
    public void SetDescription(string? description) => Description = description;
    public void ClearThumbnail() => Thumbnail = null;
    public void SetThumbnail(ImageReference thumbnail) => Thumbnail = Guard.Against.Null(thumbnail, nameof(thumbnail));    
    public void ClearPicture() => Picture = null;
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

    readonly HashSet<string> _tags = new(StringComparer.OrdinalIgnoreCase);
    private HashSet<string> _topicCategories = new(StringComparer.OrdinalIgnoreCase);


    #endregion
}