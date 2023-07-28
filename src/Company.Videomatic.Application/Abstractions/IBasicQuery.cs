namespace Company.Videomatic.Application.Abstractions;

public interface IBasicQuery
{
    string? SearchText { get; }
    string? OrderBy { get; }
    int? Skip { get; }
    int? Take { get; }
}
