using Company.SharedKernel.Abstractions;

namespace Company.SharedKernel;

public class ImageReference : ValueObject, IImageReference
{
    public static ImageReference Empty => new(string.Empty, 0, 0);

    public ImageReference(string location, int height, int width)
    {
        Url = location;
        Height = height;
        Width = width;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Url.ToUpper(); // Case insensitive
        yield return Height;
        yield return Width;
    }

    public string Url { get; private set; } = default!;
    public int Height { get; private set; }
    public int Width { get; private set; }

    #region Private 

    private ImageReference()
    { }

    #endregion
}