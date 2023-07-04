namespace Company.Videomatic.Application.Features.Videos.Commands;

public record SetVideoTags(long VideoId, string[] Tags) : IRequest<SetVideoTagsResponse>;

public record SetVideoTagsResponse(long VideoId, int Count);

public class SetVideoTagsValidator : AbstractValidator<SetVideoTags>
{
    public SetVideoTagsValidator()
    {
        RuleFor(x => x.VideoId).GreaterThan(0);
        RuleFor(x => x.Tags).NotEmpty();
    }
}