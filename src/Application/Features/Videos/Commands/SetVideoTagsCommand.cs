namespace Application.Features.Videos.Commands;

public class SetVideoTagsCommand(int Id, string[] Tags) : IRequest<Result>
{
    public int Id { get; } = Id;
    public string[] Tags { get; } = Tags;

    internal class SetVideoTagsValidator : AbstractValidator<SetVideoTagsCommand>
    {
        public SetVideoTagsValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Tags).NotNull();
        }
    }
}