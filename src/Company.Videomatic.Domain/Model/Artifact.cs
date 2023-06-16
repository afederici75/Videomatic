namespace Company.Videomatic.Domain.Model;

public class Artifact : EntityBase<int>
{    
    public string Title { get; set; }
    public string? Text { get; set; }
    public int Size => Text?.Length ?? 0;

    public Artifact(string title, string? text = default)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException($"'{nameof(title)}' cannot be null or whitespace.", nameof(title));
        }

        Title = title;
        Text = text;
    }
}