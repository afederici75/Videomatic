namespace Company.Videomatic.Application.Features.Videos.Commands;

/// <summary>
/// This command is used to update a video in the repository.
/// </summary>
public record UpdateVideoCommand(
    long Id, 
    string Title, 
    string? Description = default) : IRequest<UpdatedResponse>;


internal class UpdateVideoCommandValidator : AbstractValidator<UpdateVideoCommand>
{
    public UpdateVideoCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Title).NotEmpty();
    }
}