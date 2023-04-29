using FluentValidation;

namespace Company.Videomatic.Application.Features.Videos.DeleteVideo;

public class DeleteVideoCommandValidator : AbstractValidator<DeleteVideoCommand>
{
    public DeleteVideoCommandValidator()
    {
        RuleFor(x => x.VideoId).GreaterThan(0);
    }
}