using AutoMapper;
using Company.Videomatic.Application.Features.Artifacts;
using Company.Videomatic.Application.Features.Playlists;
using Company.Videomatic.Application.Features.Transcripts;
using Company.Videomatic.Application.Features.Videos;
using Company.Videomatic.Domain.Aggregates.Artifact;
using Company.Videomatic.Domain.Aggregates.Playlist;
using Company.Videomatic.Domain.Aggregates.Transcript;
using Company.Videomatic.Domain.Aggregates.Video;

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
            .ForPath(dest => dest.Details.PlaylistId, opt => opt.MapFrom(src => src.PlaylistId))
            .ForPath(dest => dest.Details.ChannelId, opt => opt.MapFrom(src => src.ChannelId))
            .ForPath(dest => dest.Details.VideoOwnerChannelId, opt => opt.MapFrom(src => src.VideoOwnerChannelId))
            .ForPath(dest => dest.Details.VideoOwnerChannelTitle, opt => opt.MapFrom(src => src.VideoOwnerChannelTitle))
            .ForPath(dest => dest.Details.VideoPublishedAt, opt => opt.MapFrom(src => src.VideoPublishedAt))
            .ForPath(dest => dest.Details.Provider, opt => opt.MapFrom(src => src.Provider));
        CreateMap<UpdateVideoCommand, Video>();

        // Transcripts
        CreateMap<CreateTranscriptCommand, Transcript>()
            .ForPath(dest => dest.Lines, opt => opt.MapFrom(src => src.Lines));

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
