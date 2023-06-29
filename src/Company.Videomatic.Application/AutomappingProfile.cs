using AutoMapper;
using Company.Videomatic.Domain.Playlists;
using Company.Videomatic.Domain.Videos;

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

        CreateMap<Playlist, PlaylistDTO>();
        CreateMap<Video, VideoDTO>();
        CreateMap<Thumbnail, ThumbnailDTO>();

        CreateMap<ThumbnailPayload, Thumbnail>();
        CreateMap<TranscriptPayload, Transcript>();
        CreateMap<TranscriptLinePayload, TranscriptLine>();
    }
}
