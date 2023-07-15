namespace Company.Videomatic.Application.Features.Videos.Commands;

public record SetVideoTags(long Id, string[] Tags) : IRequest<SetVideoTagsResponse>;

public record SetVideoTagsResponse(long VideoId, int Count);

public class SetVideoTagsValidator : AbstractValidator<SetVideoTags>
{
    public SetVideoTagsValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Tags).NotEmpty();
    }
}