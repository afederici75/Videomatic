namespace Company.Videomatic.Application.Features.Playlists;

/// <summary>
/// Deletes an existing collection.
/// </summary>
/// <param name="id"></param>
public record DeletePlaylistCommand(int Id) : IRequest<DeletePlaylistResponse>;

/// <summary>
/// This response is returned by DeleteCollectionCommand.
/// </summary>
/// <param name="Item"></param>
/// <param name="Deleted"></param>
public record DeletePlaylistResponse(bool Deleted);

/// <summary>
/// This event is published when a video is deleted.
/// </summary>
public record PlaylistDeletedEvent(Playlist Item) : INotification;
