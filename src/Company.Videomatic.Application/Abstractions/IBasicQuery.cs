namespace Company.Videomatic.Application.Abstractions;

public enum FullTextSearchType
{ 
    FreeText,
    Contains
}

public interface IBasicQuery
{
    string? SearchText { get; }
    FullTextSearchType? SearchType { get; }
    string? OrderBy { get; }
    int? Skip { get; }
    int? Take { get; }
}

public static class BasicQueryExtensions
{
    public static void Foo(this IBasicQuery query)
    { 
        
    }
}