using AutoMapper;

namespace Company.Videomatic.Application;

public class AutomappingProfile : Profile
{
    public AutomappingProfile()
    {
        // Playlists
        CreateMap<CreatePlaylistCommand, Playlist>();
        CreateMap<UpdatePlaylistCommand, Playlist>();

        // Videos
        CreateMap<CreateVideoCommand, Video>()
            //.ForPath(dest => dest.Details.PlaylistId, opt => opt.MapFrom(src => src.PlaylistId))
            //.ForPath(dest => dest.Details.ChannelId, opt => opt.MapFrom(src => src.ChannelId))
            .ForPath(dest => dest.Details.VideoOwnerChannelId, opt => opt.MapFrom(src => src.VideoOwnerChannelId))
            .ForPath(dest => dest.Details.VideoOwnerChannelTitle, opt => opt.MapFrom(src => src.VideoOwnerChannelTitle))
            .ForPath(dest => dest.Details.VideoPublishedAt, opt => opt.MapFrom(src => src.VideoPublishedAt))
            .ForPath(dest => dest.Details.Provider, opt => opt.MapFrom(src => src.Provider));
        CreateMap<UpdateVideoCommand, Video>();

        // Transcripts
        CreateMap<CreateTranscriptCommand, Transcript>()
            //.ForPath(dest => dest.Lines, opt => opt.MapFrom(src => src.Lines))
            .ForMember(dest => dest.Lines, opt => opt.MapFrom(src => src.Lines.Select(TranscriptLine.FromString)));

        CreateMap<DeleteTranscriptCommand, Transcript>();
        // See https://stackoverflow.com/questions/24809956/automapper-and-mapping-list-within-a-complex-object-nested-mappings
        //CreateMap<string, TranscriptLine>()
        //    .ConstructUsing((val, ct) => ct.Mapper.Map<TranscriptLine>(val))
        //    .ForAllMembers(opt => opt.Ignore());

        CreateMap<string, TranscriptLine>();
        CreateMap<TranscriptLine, string>();

        // Artifacts
        CreateMap<CreateArtifactCommand, Artifact>();
        CreateMap<UpdateArtifactCommand, Artifact>();

        // DTOs
        CreateMap<Playlist, PlaylistDTO>();
        CreateMap<Video, VideoDTO>();
        CreateMap<Thumbnail, ThumbnailDTO>();
        CreateMap<Transcript, TranscriptDTO>();
        CreateMap<Artifact, ArtifactDTO>();

        //CreateMap<PageResult<Video>, PageResult<VideoDTO>>();
    }
}
