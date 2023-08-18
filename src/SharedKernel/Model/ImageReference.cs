namespace SharedKernel.Model;

public class ImageReference(
    string url,
    int height,
    int width) : ValueObject
{
    public static ImageReference Empty => new("", 0, 0);

    public string Url { get; } = url;
    public int Height { get; } = height;
    public int Width { get; } = width;


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Url.ToUpper(); // Case insensitive
        yield return Height;
        yield return Width;
    }
}