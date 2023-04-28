namespace Company.Videomatic.Application.Features.Videos.Commands.ImportVideo;

public class ImportVideoCommandValidator : AbstractValidator<ImportVideoCommand>
{
    public ImportVideoCommandValidator()
    {
        RuleFor(v => v.VideoUrl).NotEmpty().WithMessage("VideoUrl is required.");
    }
}   