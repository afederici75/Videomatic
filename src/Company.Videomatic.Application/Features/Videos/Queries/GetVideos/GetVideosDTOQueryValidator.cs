﻿namespace Company.Videomatic.Application.Features.Videos.Queries;

public class GetVideosDTOQueryValidator : AbstractValidator<GetVideosDTOQuery>
{
    public GetVideosDTOQueryValidator()
    {
        RuleFor(x => x.Take).GreaterThan(0);
        RuleFor(x => x.Skip).GreaterThanOrEqualTo(0);
    }
}
