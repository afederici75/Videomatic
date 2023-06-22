namespace Company.Videomatic.Application.Features.DataAccess;

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

internal abstract class FilterValidatorBase<TFILTER> : AbstractValidator<TFILTER?>
    where TFILTER : Filter
{
    // TODO: dubious location for this class
    public static class Lengths
    {
        public const int MaxSearchTextLength = 128;
    }

    public FilterValidatorBase()
    {
        RuleFor(x => x!.SearchText).MaximumLength(128); // Works only if SearchText != null

        When(x => x!.Ids != null, () =>
        {
            RuleFor(x => x!.Ids).NotEmpty();
            RuleForEach(x => x!.Ids).GreaterThanOrEqualTo(1);
        });

        When(x => x!.Items != null, () =>
        {
            RuleFor(x => x!.Items).NotEmpty();
            RuleForEach(x => x!.Items).SetValidator(new FilterItemValidator());
        });

        RuleFor(x => x).Must(x => ValidateObjectState(x!)).WithMessage("A filter must contain have at least one parameter set.");
    }

    protected virtual bool ValidateObjectState(TFILTER x)
    {
        return x!.Ids != null || x!.Items != null || x!.SearchText != null;
    }
}

internal class FilterValidator : FilterValidatorBase<Filter>
{
    public FilterValidator() : base() { }
}

#endregion