namespace Application.Abstractions;


// TODO: never truly used IBasicQuery
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