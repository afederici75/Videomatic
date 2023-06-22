﻿namespace Company.Videomatic.Application.Query;

public record Paging(int Page, int PageSize);

public class PagingValidator : AbstractValidator<Paging?>
{
    public PagingValidator()
    {
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1);
        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
    }
}