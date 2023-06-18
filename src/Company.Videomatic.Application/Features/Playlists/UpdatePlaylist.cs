namespace Company.Videomatic.Application.Features.Playlists;

public record UpdatePlaylistCommand(int Id, string Name, string Description) : IRequest<UpdatePlaylistResponse>;

public record UpdatePlaylistResponse(int Id, bool Updated);

/// <summary>
/// This event is published when a video is updated.
/// </summary>
/// <param name="VideoId"></param>
public record PlaylistUpdatedEvent(int Id);