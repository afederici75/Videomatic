namespace Company.Videomatic.Application.Specifications;

// test for now 

public class PlaylistFiltered : Specification<Playlist, PlaylistDTO>
{
    public PlaylistFiltered(PlaylistId playlistId)
    {
        Query.Select(p => new PlaylistDTO(p.Id, p.Name, p.Description, -1))
             .Where(p => p.Id == playlistId);
        //    .sele
    }
}
