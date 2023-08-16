using SharedKernel.Model;

namespace Application.Features.Playlists;

public readonly record struct PlaylistDTO(
    int Id,
    string Name,
    ImageReference Thumbnail,
    ImageReference Picture,
    string? Description,
    int VideoCount);