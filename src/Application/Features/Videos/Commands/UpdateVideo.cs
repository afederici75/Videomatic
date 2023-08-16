using SharedKernel.CQRS.Commands;
using Domain.Videos;

namespace Application.Features.Videos.Commands;

/// <summary>
/// This command is used to update a video in the repository.
/// </summary>
public readonly record struct UpdateVideoCommand(
    VideoId Id, 
    string Name, 
    string? Description = default) : IRequest<Result<Video>>;


internal class UpdateVideoCommandValidator : AbstractValidator<UpdateVideoCommand>
{
    public UpdateVideoCommandValidator()
    {
        RuleFor(x => (int)x.Id).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty();
    }
}