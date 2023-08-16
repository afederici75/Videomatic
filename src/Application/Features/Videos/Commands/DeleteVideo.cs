using SharedKernel.CQRS.Commands;
using Domain.Videos;

namespace Application.Features.Videos.Commands;

/// <summary>
/// This command is used to delete a video from the repository.
/// </summary>
/// <param name="VideoId"> The id of the video to delete. </param>
public readonly record struct DeleteVideoCommand(VideoId Id) : IRequest<Result>;

/// <summary>
/// The validator for DeleteVideoCommand.
/// </summary>
internal class DeleteVideoCommandValidator : AbstractValidator<DeleteVideoCommand>
{
    public DeleteVideoCommandValidator()
    {
        RuleFor(x => (int)x.Id).GreaterThan(0);
    }
}