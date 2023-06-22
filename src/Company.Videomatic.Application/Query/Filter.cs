using Company.Videomatic.Application.Features.Playlists.Queries;
using System.Linq.Expressions;

namespace Company.Videomatic.Application.Query;

public record Filter(
    string? SearchText = null,
    long[]? Ids = null,
    FilterItem[]? Items = null)
{ 
    //public Filter(params FilterItem[] Items) : this(null, null, Items) { }
}

public class FilterValidator<TFILTER> : AbstractValidator<TFILTER?>
    where TFILTER : Filter
{
    public FilterValidator()
    {
        RuleFor(x => x!.SearchText).NotNull().When(x => (x!.Ids == null) && (x!.Items == null));
        RuleFor(x => x!.Ids).NotNull().When(x => (x!.SearchText == null) && (x!.Items == null));
        RuleFor(x => x!.Items).NotNull().When(x => (x!.Ids == null) && (x!.SearchText == null));

    }
}