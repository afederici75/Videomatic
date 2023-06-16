namespace Company.Videomatic.Domain.Model;

public class Artifact : EntityBase
{    
    public string Title { get; private set; }
    public string Type { get; private set; }
    public string? Text { get; private set; }    

    public Artifact(string title, string type, string? text = default)
    {
        Title = Guard.Against.NullOrWhiteSpace(title, nameof(title));
        Type = Guard.Against.NullOrWhiteSpace(type, nameof(type));  
        Text = text;
    }

    #region Methods

    public Artifact UpdateTitle(string newTitle)
    {
        Title = Guard.Against.NullOrWhiteSpace(newTitle, nameof(newTitle));

        return this;
    }

    public Artifact UpdateText(string newText)
    {
        Text = newText;

        return this;
    }
    
    #endregion

    #region Private

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [JsonConstructor]
    private Artifact()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        // For entity framework
    }

    #endregion
}