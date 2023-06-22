using Company.Videomatic.Application.Features.DataAccess;

namespace Company.Videomatic.Application.Features.Videos.Commands;

/// <summary>
/// This command is used to update a video in the repository.
/// </summary>
public record UpdateVideoCommand(
    int Id, 
    string Title, 
    string? Description = default) : IRequest<UpdatedResponse>;