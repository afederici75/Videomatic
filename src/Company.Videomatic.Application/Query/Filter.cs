namespace Company.Videomatic.Application.Query;

/// <summary>
/// A query filter. It provides a way to filter the results of a query by a search text, a list of ids or a list of items.
/// </summary>
/// <param name="SearchText">A search text.</param>
/// <param name="Ids">A list of ids.</param>
/// <param name="Items">A list of property filters.</param>
public record Filter(
    string? SearchText = null,
    long[]? Ids = null,
    FilterItem[]? Items = null);

#region Validator

internal class FilterValidator<TFILTER> : AbstractValidator<TFILTER?>
    where TFILTER : Filter
{
    // TODO: dubious location for this class
    public static class Lengths
    {        
        public const int MaxSearchTextLength = 128;        
    }

    public FilterValidator()
    {
        When(x => (x!.Ids == null) && (x!.Items == null) && (string.IsNullOrWhiteSpace(x.SearchText)),
            () =>
            {
                const string ComboMessage = "SearchText, Ids or Items must be specified";
                RuleFor(x => x!.SearchText).NotEmpty().WithMessage(ComboMessage);                
            })
        .Otherwise(() => 
        {
            RuleFor(x => x!.SearchText).MaximumLength(128); // Works only if SearchText != null
            
            When(x => x!.Ids != null, () =>
            {
                RuleFor(x => x!.Ids).NotEmpty();
            });

            When(x => x!.Items != null, () =>
            {
                RuleFor(x => x!.Items).NotEmpty();
                RuleForEach(x => x!.Items).SetValidator(new FilterItemValidator()); 
            });            
        });
    }
}

#endregion