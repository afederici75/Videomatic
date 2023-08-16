using SharedKernel.Model;

namespace Application.Features.Playlists;

public record PlaylistDTO(
    int Id,
    string Name,
    ImageReference Thumbnail,
    ImageReference Picture,
    string? Description,
    int VideoCount);