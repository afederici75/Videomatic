namespace Company.Videomatic.Application.Features.Videos.Commands;

public record SetVideoTags(int Id, string[] Tags) : IRequest<Result<int>>;

public class SetVideoTagsValidator : AbstractValidator<SetVideoTags>
{
    public SetVideoTagsValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Tags).NotEmpty();
    }
}