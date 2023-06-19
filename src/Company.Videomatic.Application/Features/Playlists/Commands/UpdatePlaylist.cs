namespace Company.Videomatic.Application.Features.Playlists.Commands;

public record UpdatePlaylistCommand(long Id, string Name, string? Description) : IRequest<UpdatePlaylistResponse>;

public record UpdatePlaylistResponse(long Id, bool Updated);

/// <summary>
/// This event is published when a video is updated.
/// </summary>
/// <param name="VideoId"></param>
public record PlaylistUpdatedEvent(int Id);