using FluentValidation;

namespace Company.Videomatic.Application.Query;

public record Filter(
    string? SearchText = null,
    long[]? Ids = null,
    FilterItem[]? Items = null);

public class FilterValidator<TFILTER> : AbstractValidator<TFILTER?>
    where TFILTER : Filter
{
    public static class Lengths
    {
        public const int MaxSearchTextLength = 128;
    }

    public FilterValidator()
    {
        When(x => (x!.Ids == null) && (x!.Items == null) && (string.IsNullOrWhiteSpace(x.SearchText)),
            () =>
            {
                const string ErrorMessage = "SearchText, Ids or Items must be specified";
                RuleFor(x => x!.SearchText).NotEmpty().WithMessage(ErrorMessage);
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
            });            
        });
    }
}