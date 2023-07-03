using Ardalis.Result;
using AutoMapper;
using Company.Videomatic.Domain.Abstractions;
using Company.Videomatic.Domain.Aggregates.Artifact;
using Company.Videomatic.Domain.Aggregates.Playlist;
using Company.Videomatic.Domain.Aggregates.Transcript;
using Company.Videomatic.Domain.Aggregates.Video;

namespace Microsoft.Extensions.DependencyInjection;

public class AutomappingProfile : Profile
{
    public AutomappingProfile()
    {
        CreateMap<CreatePlaylistCommand, Playlist>();
        CreateMap<UpdatePlaylistCommand, Playlist>();

        CreateMap<CreateVideoCommand, Video>()
            .ForPath(dest => dest.Details.PlaylistId, opt => opt.MapFrom(src => src.PlaylistId))
            .ForPath(dest => dest.Details.ChannelId, opt => opt.MapFrom(src => src.ChannelId))
            .ForPath(dest => dest.Details.VideoOwnerChannelId, opt => opt.MapFrom(src => src.VideoOwnerChannelId))
            .ForPath(dest => dest.Details.VideoOwnerChannelTitle, opt => opt.MapFrom(src => src.VideoOwnerChannelTitle))
            .ForPath(dest => dest.Details.VideoPublishedAt, opt => opt.MapFrom(src => src.VideoPublishedAt))
            .ForPath(dest => dest.Details.Provider, opt => opt.MapFrom(src => src.Provider));

        CreateMap<UpdateVideoCommand, Video>();

        CreateMap<UpdateTranscriptCommand, Transcript>()
            .ForPath(dest => dest.Lines, opt => opt.MapFrom(src => src.Lines));
        CreateMap<TranscriptLinePayload, TranscriptLine>();

        CreateMap<UpdateArtifactCommand, Artifact>();

        CreateMap<Playlist, PlaylistDTO>();
        CreateMap<Video, VideoDTO>();
        CreateMap<Thumbnail, ThumbnailDTO>();

        CreateMap<ThumbnailInfo, Thumbnail>();

        CreateMap<PageResult<Video>, PageResult<VideoDTO>>();
    }
}
