using Company.Videomatic.Application.Features.Model;
using Company.Videomatic.Infrastructure.Data.Model;

namespace Microsoft.Extensions.DependencyInjection;

public class AutomappingProfile : Profile
{
    public AutomappingProfile()
    {
        CreateMap<CreatePlaylistCommand, Playlist>();
        CreateMap<UpdatePlaylistCommand, Playlist>();
        
        CreateMap<CreateVideoCommand, Video>();
        CreateMap<UpdateVideoCommand, Video>();

        CreateMap<Playlist, PlaylistDTO>();
        CreateMap<Video, VideoDTO>();        
    }
}
