using Company.SharedKernel.CQRS.Commands;
using Company.Videomatic.Domain.Video;

namespace Company.Videomatic.Application.Features.Videos.Commands;

/// <summary>
/// This command is used to update a video in the repository.
/// </summary>
public record UpdateVideoCommand(
    int Id, 
    string Name, 
    string? Description = default) : UpdateEntityCommand<Video>(Id);


internal class UpdateVideoCommandValidator : AbstractValidator<UpdateVideoCommand>
{
    public UpdateVideoCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty();
    }
}