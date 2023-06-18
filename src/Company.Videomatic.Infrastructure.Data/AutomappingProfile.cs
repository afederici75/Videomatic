﻿using Company.Videomatic.Application.Features.Playlists;
using Company.Videomatic.Application.Features.Videos;

namespace Microsoft.Extensions.DependencyInjection;

public class AutomappingProfile : Profile
{
    public AutomappingProfile()
    {
        CreateMap<CreatePlaylistCommand, PlaylistDb>();
        CreateMap<CreateVideoCommand, VideoDb>();
        //CreateMap<PlaylistDb, CreatePlaylistResponse>();

        //
        CreateMap<Artifact, ArtifactDb>();
        CreateMap<EntityBase, EntityBaseDb>();
        CreateMap<Playlist, PlaylistDb>();
        CreateMap<Tag, TagDb>();
        CreateMap<Thumbnail, ThumbnailDb>();
        CreateMap<Transcript, TranscriptDb>();
        CreateMap<TranscriptLine, TranscriptLineDb>();
        CreateMap<Video, VideoDb>();

        CreateMap<ArtifactDb, Artifact>();
        CreateMap<EntityBaseDb, EntityBase>();
        CreateMap<PlaylistDb, Playlist>();
        CreateMap<TagDb, Tag>();
        CreateMap<ThumbnailDb, Thumbnail>();
        CreateMap<TranscriptDb, Transcript>();
        CreateMap<TranscriptLineDb, TranscriptLine>();
        CreateMap<VideoDb, Video>();
    }
}
