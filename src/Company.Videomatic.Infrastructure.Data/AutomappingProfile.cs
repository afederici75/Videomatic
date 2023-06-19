using Company.Videomatic.Application.Features.Playlists;
using Company.Videomatic.Application.Features.Playlists.Commands;
using Company.Videomatic.Application.Features.Videos;
using Company.Videomatic.Application.Features.Videos.Commands;

namespace Microsoft.Extensions.DependencyInjection;

public class AutomappingProfile : Profile
{
    public AutomappingProfile()
    {
        CreateMap<CreatePlaylistCommand, PlaylistDb>();
        CreateMap<UpdatePlaylistCommand, PlaylistDb>();
        CreateMap<CreateVideoCommand, VideoDb>();
        CreateMap<PlaylistDb, PlaylistDTO>();
        CreateMap<VideoDb, VideoDTO>();

        //
        //CreateMap<Artifact, ArtifactDb>();
        //CreateMap<EntityBase, EntityBaseDb>();
        //CreateMap<Playlist, PlaylistDb>();
        //CreateMap<Tag, TagDb>();
        //CreateMap<Thumbnail, ThumbnailDb>();
        //CreateMap<Transcript, TranscriptDb>();
        //CreateMap<TranscriptLine, TranscriptLineDb>();
        //CreateMap<Video, VideoDb>();

        //CreateMap<ArtifactDb, Artifact>();
        //CreateMap<EntityBaseDb, EntityBase>();
        //CreateMap<PlaylistDb, Playlist>();
        //CreateMap<TagDb, Tag>();
        //CreateMap<ThumbnailDb, Thumbnail>();
        //CreateMap<TranscriptDb, Transcript>();
        //CreateMap<TranscriptLineDb, TranscriptLine>();
        //CreateMap<VideoDb, Video>();

        
    }
}
