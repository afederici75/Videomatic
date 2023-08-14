namespace Company.SharedKernel.Abstractions;

public interface IImageReference
{
    int Height { get; }
    string Url { get; }
    int Width { get; }
}