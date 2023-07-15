namespace Company.Videomatic.Application.Features.Videos.Commands;

/// <summary>
/// This command is used to delete a video from the repository.
/// </summary>
/// <param name="VideoId"> The id of the video to delete. </param>
public record DeleteVideoCommand(long Id) : DeleteAggregateRootCommand<Video>(Id);

/// <summary>
/// The validator for DeleteVideoCommand.
/// </summary>
internal class DeleteVideoCommandValidator : AbstractValidator<DeleteVideoCommand>
{
    public DeleteVideoCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
    }
}