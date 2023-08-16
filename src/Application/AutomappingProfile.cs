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
        CreateMap<Transcript, TranscriptDTO>();
        CreateMap<Artifact, ArtifactDTO>();        
    }
}
