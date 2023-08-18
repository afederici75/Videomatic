namespace SharedKernel.Model;

public class NameAndDescription(
    string name,
    string? description) : ValueObject
{
    public static NameAndDescription Empty => new("", null);

    public string Name { get; } = name;
    public string? Description { get; } = description;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name.ToUpper(); // Case insensitive        
    }
}
