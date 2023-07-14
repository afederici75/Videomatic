﻿using Application.Tests.Helpers;
using Ardalis.Result;
using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Application.Features.Videos;
using Company.Videomatic.Domain.Abstractions;
using Company.Videomatic.Domain.Aggregates.Playlist;
using Company.Videomatic.Domain.Aggregates.Video;
using Infrastructure.Data.Tests.Helpers;
using Newtonsoft.Json;
using System.Text.Json;

namespace Infrastructure.Data.Tests;

[Collection("DbContextTests")]
public class VideosTests : IClassFixture<DbContextFixture>
{
    public VideosTests(
        DbContextFixture fixture,
        ISender sender)
    {
        Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        Sender = sender ?? throw new ArgumentNullException(nameof(sender));

        Fixture.SkipDeletingDatabase = true;
    }

    public DbContextFixture Fixture { get; }
    public ISender Sender { get; }

    [Fact]
    public async Task CreateVideo()
    {
        var createCommand = CreateVideoCommandBuilder.WithRandomValuesAndEmptyVideoDetails();

        var response = await Sender.Send(createCommand);

        // Checks
        response.Value.Id.Value.Should().BeGreaterThan(0);

        // Just a small test to see if LINQ creates a simpler query than
        // the one with all owned properties just down below.
        //var tmp = await Fixture.DbContext.Videos.Where(x => x.Id == response.Id).Select(x=> x.Id).SingleAsync();
        var video = Fixture.DbContext.Videos.Single(x => x.Id == response.Value.Id);
        
        video.Name.Should().BeEquivalentTo(createCommand.Name);
        video.Description.Should().BeEquivalentTo(createCommand.Description);
        video.Location.Should().BeEquivalentTo(createCommand.Location);
        //video.Details.ChannelId.Should().BeEquivalentTo(createCommand.ChannelId); 
        //video.Details.PlaylistId.Should().BeEquivalentTo(createCommand.PlaylistId);
        video.Details.Provider.Should().BeEquivalentTo(createCommand.Provider);
        video.Details.VideoOwnerChannelId.Should().BeEquivalentTo(createCommand.VideoOwnerChannelId);
        video.Details.VideoOwnerChannelTitle.Should().BeEquivalentTo(createCommand.VideoOwnerChannelTitle);
        video.Details.VideoPublishedAt.Should().Be(createCommand.VideoPublishedAt);        
    }

    [Fact]
    public async Task DeleteVideo()
    {
        var videoId = await Sender.Send(CreateVideoCommandBuilder.WithRandomValuesAndEmptyVideoDetails());

        // Executes
        var deletedResponse = await Sender.Send(new DeleteVideoCommand(videoId.Value.Id));

        // Checks
        videoId.Value.Id.Value.Should().BeGreaterThan(0);

        var row = await Fixture.DbContext.Videos.FirstOrDefaultAsync(x => x.Id == videoId.Value.Id);

        row.Should().BeNull();
    }

    [Fact]
    public async Task UpdateVideo()
    {
        var createResponse = await Sender.Send(
            CreateVideoCommandBuilder.WithRandomValuesAndEmptyVideoDetails());

        var updateCommand = new UpdateVideoCommand(
            createResponse.Value.Id,
            "New Title",
            "New Description");

        Result<Video> response = await Sender.Send(updateCommand);

        // Checks
        var video = await Fixture.DbContext.Videos
            .Where(x => x.Id == createResponse.Value.Id)
            .SingleAsync();

        video.Id.Value.Should().Be(updateCommand.Id);
        video.Name.Should().BeEquivalentTo(updateCommand.Name);
        video.Description.Should().BeEquivalentTo(updateCommand.Description);        
    }

    [Fact]
    public async Task LinksTwoVideoToPlaylist()
    {
        // Prepares
        var createPlaylistCmd = new CreatePlaylistCommand(
            Name: nameof(LinksTwoVideoToPlaylist),
            Description: $"A description for my playlist {DateTime.Now}");

        Result<Playlist> playlist1 = await Sender.Send(createPlaylistCmd);

        var createVid1Cmd = CreateVideoCommandBuilder
            .WithRandomValuesAndEmptyVideoDetails(nameof(LinksTwoVideoToPlaylist) + "V1");

        Result<Video> video1 = await Sender.Send(createVid1Cmd);

        var createVid2Cmd = CreateVideoCommandBuilder
            .WithRandomValuesAndEmptyVideoDetails(nameof(LinksTwoVideoToPlaylist) + "V2");

        throw new Exception("Finish this test");
        // Executes
        //var addVidsCmd = new LinkVideoToPlaylistsCommand(
        //    video1,
        //    new long[] { playlist1 });
        //
        //LinkVideoToPlaylistsResponse addVidsResponse = await Sender.Send(addVidsCmd); // Should add 2 videos
        //LinkVideoToPlaylistsResponse emptyAddVidsResponse = await Sender.Send(addVidsCmd); // Should not add anything as they are both dups
        //
        //// Checks
        //playlist1.Value.Should().BeGreaterThan(0);
        //video1.Value.Should().BeGreaterThan(0);
    }

    [Theory]
    [InlineData(null, null, null, true, null, 2)]
    //[InlineData(new long[] { 1 }, null, null, true, null, 2)]
    //[InlineData(new long[] { 1, 2 }, null, null, true, null, 2)]
    public async Task GetVideos(
        long[]? playlistIds,
        string? searchText,
        string? orderBy,
        bool includeCounts,
        ThumbnailResolutionDTO? IncludeThumbnail,
        int expectedResults)
    {
        var query = new GetVideosQuery(
            PlaylistIds: playlistIds, 
            //VideoIds: videoIds, 
            SearchText: searchText, 
            OrderBy: orderBy, 
            Page: null, // Uses 1 by default
            PageSize: null, // Uses 10 by default
            IncludeCounts: includeCounts, 
            Resolution: IncludeThumbnail);

        PageResult<VideoDTO> response = await Sender.Send(query);

        // Checks
        response.Count.Should().Be(expectedResults);
        response.TotalCount.Should().Be(expectedResults);
        if (includeCounts)
        {            
        }
    }

    [Theory]
    [InlineData(new long[] { 1 }, null, 1)]
    [InlineData(new long[] { 1, 2 }, null, 2)]
    [InlineData(new long[] { 2 }, null, 1)]
    [InlineData(new long[] { 3 }, null, 0)]
    // TODO: missing paging tests and should add more anyway
    public async Task GetVideosById(
        long[] videoIds,        
        ThumbnailResolutionDTO? IncludeThumbnail,
        int expectedResults)
    {
        var query = new GetVideosByIdQuery(
            VideoIds: videoIds,             
            Resolution: IncludeThumbnail);

        var items = await Sender.Send(query);

        // Checks
        items.Should().HaveCount(expectedResults);        
    }

    [Theory(Skip ="Expensive!")]
    [InlineData("TestData//Video-n1kmKpjk_8E.json", null)]
    public async Task ImportOneVideo(string fileName, [FromServices] IRepository<Video> repository)
    {
        var json = await File.ReadAllTextAsync(fileName);
        var video = JsonConvert.DeserializeObject<Video>(json)!;

        video.Location.Should().NotBeNullOrEmpty();
        video.Tags.Should().HaveCount(25);

        await repository.AddAsync(video);
    }

    [Theory(Skip = "Expensive!")]
    [InlineData("TestData//Playlist-PLLdi1lheZYVKkvX20ihB7Ay2uXMxa0Q5e.json", null, null, null)]
    public async Task ImportPlaylist(string fileName, 
        [FromServices] IRepository<Video> videoRepository,
        [FromServices] IRepository<Playlist> playListRepository,
        [FromServices] IVideoService videoService
        )
    {
        var json = await File.ReadAllTextAsync(fileName);
        var videos = JsonConvert.DeserializeObject<Video[]>(json)!;

        #region One by one (debug)
        //var idx = 0;
        //foreach (var video in videos)
        //{
        //    try
        //    {
        //        await videoRepository.AddAsync(video);
        //        idx++;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }   
        //}
        #endregion

        await videoRepository.AddRangeAsync(videos);

        // Links to playlist
        var pl = Playlist.Create(nameof(ImportPlaylist));
        await playListRepository.AddAsync(pl);  

        foreach (var v in videos)
        {
            await videoService.LinkToPlaylists(v.Id.Value, new [] { pl.Id });
        }       
    }
}