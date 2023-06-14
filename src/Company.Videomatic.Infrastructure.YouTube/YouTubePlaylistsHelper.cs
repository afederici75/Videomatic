using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Videomatic.Infrastructure.YouTube;

public record Playlist(string Id, string Title, string Description);

public interface IPlaylistsHelper
{
    IAsyncEnumerable<Playlist> GetPlaylists();
}

public class YouTubePlaylistsHelper : IPlaylistsHelper
{
    public YouTubePlaylistsHelper()
    {
            
    }

    public async IAsyncEnumerable<Playlist> GetPlaylists()
    { 
        for (var i = 0; i < 10; i++) 
        {
            yield return new Playlist(i.ToString(), "Title", "Description");
        }
    }
}