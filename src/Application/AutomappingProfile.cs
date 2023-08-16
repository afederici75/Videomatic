using SharedKernel.Model;

namespace Application;

public class AutomappingProfile : Profile
{
    public AutomappingProfile()
    {
        // Playlists
        CreateMap<CreatePlaylistCommand, Playlist>();
        CreateMap<UpdatePlaylistCommand, Playlist>();
        CreateMap<DeletePlaylistCommand, Playlist>();
        // Videos

        CreateMap<CreateVideoCommand, Video>()
            .ForPath(dest => dest.Origin!.ProviderId, opt => opt.MapFrom(src => src.ProviderId))
            .ForPath(dest => dest.Origin!.ProviderItemId, opt => opt.MapFrom(src => src.))
            .ForPath(dest => dest.Origin!.ChannelId, opt => opt.MapFrom(src => src.ChannelId))
            .ForPath(dest => dest.Origin!.ChannelName, opt => opt.MapFrom(src => src.ChannelName))
            .ForPath(dest => dest.Origin!.PublishedOn, opt => opt.MapFrom(src => src.PublishedOn))            
            .ForPath(dest => dest.Origin!., opt => opt.MapFrom(src => src.PlaylistId))
            .ForPath(dest => dest.Origin!., opt => opt.MapFrom(src => src.PictureUrl))


        CreateMap<UpdateVideoCommand, Video>();
        CreateMap<DeleteVideoCommand, Video>();
        // Transcripts
        CreateMap<CreateTranscriptCommand, Transcript>()
            .ForMember(dest => dest.Lines, opt => opt.MapFrom(src => src.Lines.Select(line => new TranscriptLine(line, null, null))));

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
        CreateMap<DeleteArtifactCommand, Artifact>();
        // DTOs
        CreateMap<Playlist, PlaylistDTO>();
        CreateMap<Video, VideoDTO>();
        CreateMap<ImageReference, ThumbnailDTO>();
        CreateMap<Transcript, TranscriptDTO>();
        CreateMap<Artifact, ArtifactDTO>();

        //CreateMap<Page<Video>, Page<VideoDTO>>();
    }
}
