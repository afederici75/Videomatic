namespace Company.Videomatic.Application.Query;

public record OrderBy(params OrderByItem[] Items)
{
}

public class OrderByValidator : AbstractValidator<OrderBy?>
{
    public OrderByValidator()
    {
        RuleForEach(x => x.Items).SetValidator(new OrderByItemValidator());
    }
}
