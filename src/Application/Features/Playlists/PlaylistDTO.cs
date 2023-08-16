using SharedKernel.Model;

namespace Application.Features.Playlists;

public class PlaylistDTO(
    int id,
    string name,
    ImageReference thumbnail,
    ImageReference picture,
    string? description,
    int videoCount)
{ 
    public int Id { get; } = id;
    public string Name { get; } = name;
    public ImageReference Thumbnail { get; } = thumbnail;
    public ImageReference Picture { get; } = picture;
    public string? Description { get; } = description;
    public int VideoCount { get; } = videoCount;

}