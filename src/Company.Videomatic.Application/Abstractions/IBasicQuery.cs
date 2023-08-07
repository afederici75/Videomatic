namespace Company.Videomatic.Application.Abstractions;

public interface IBasicQuery
{
    string? SearchText { get; }
    TextSearchType? SearchType { get; }
    string? SearchOn { get; }

    string? OrderBy { get; }

    int? Skip { get; }
    int? Take { get; }
}

public enum TextSearchType
{
    FreeText,
    Contains
}