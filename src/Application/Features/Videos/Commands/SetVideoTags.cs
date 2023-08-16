namespace Application.Features.Videos.Commands;

public readonly record struct SetVideoTags(int Id, string[] Tags) : IRequest<Result>;

internal class SetVideoTagsValidator : AbstractValidator<SetVideoTags>
{
    public SetVideoTagsValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Tags).NotNull();
    }
}