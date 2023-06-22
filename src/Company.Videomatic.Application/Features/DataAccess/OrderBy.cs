namespace Company.Videomatic.Application.Features.DataAccess;

public record OrderBy(OrderByItem[] Items)
{
}

internal class OrderByValidator : AbstractValidator<OrderBy?>
{
    public OrderByValidator()
    {
        RuleFor(x => x!.Items).NotEmpty();
        RuleForEach(x => x!.Items).SetValidator(new OrderByItemValidator());
    }
}