namespace Application.Features.Videos.Commands;

public record SetVideoTags(int Id, string[] Tags) : IRequest<Result>;

public class SetVideoTagsValidator : AbstractValidator<SetVideoTags>
{
    public SetVideoTagsValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Tags).NotNull();
    }
}