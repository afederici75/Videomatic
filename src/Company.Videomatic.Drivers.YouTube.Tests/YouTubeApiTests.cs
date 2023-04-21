using Company.Videomatic.Drivers.YouTube.Options;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.DependencyInjection;

namespace Company.Videomatic.Drivers.YouTube.Tests;

public class YouTubeApiTests
{
    [Theory]
    [InlineData(null)]
    public async Task GetAllPlaylists_ShouldReturnAllPlaylistNamesAndVideos(
        [FromServices] IOptions<YouTubeOptions> options)
    {
        try
        {
            // Create the YouTube service object using the API key.
            YouTubeService youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = options.Value.ApiKey,
                ApplicationName = options.Value.ApplicationName
            });

            // Retrieve the channel ID for the channel name specified.
            ChannelsResource.ListRequest channelsListRequest = youtubeService.Channels.List("id");
            channelsListRequest.ForUsername = options.Value.ChannelName;
            ChannelListResponse channelsListResponse = await channelsListRequest.ExecuteAsync();
            string channelId = channelsListResponse.Items[0].Id;

            // Retrieve the list of playlists for the channel ID specified.
            PlaylistsResource.ListRequest playlistsListRequest = youtubeService.Playlists.List("snippet");
            playlistsListRequest.ChannelId = channelId;
            playlistsListRequest.MaxResults = 50;
            PlaylistListResponse playlistsListResponse;
            List<Playlist> playlists = new List<Playlist>();
            do
            {
                playlistsListResponse = await playlistsListRequest.ExecuteAsync();
                playlists.AddRange(playlistsListResponse.Items);
                playlistsListRequest.PageToken = playlistsListResponse.NextPageToken;
            } while (playlistsListResponse.NextPageToken != null);

            // Retrieve the videos for each playlist.
            foreach (Playlist playlist in playlists)
            {
                PlaylistItemsResource.ListRequest playlistItemsListRequest = youtubeService.PlaylistItems.List("snippet");
                playlistItemsListRequest.PlaylistId = playlist.Id;
                playlistItemsListRequest.MaxResults = 50;
                PlaylistItemListResponse playlistItemsListResponse;
                List<PlaylistItem> playlistItems = new List<PlaylistItem>();
                do
                {
                    playlistItemsListResponse = await playlistItemsListRequest.ExecuteAsync();
                    playlistItems.AddRange(playlistItemsListResponse.Items);
                    playlistItemsListRequest.PageToken = playlistItemsListResponse.NextPageToken;
                } while (playlistItemsListResponse.NextPageToken != null);

                // Assert that the playlist name and video titles are not null or empty.
                Assert.False(string.IsNullOrEmpty(playlist.Snippet.Title));
                foreach (PlaylistItem playlistItem in playlistItems)
                {
                    Assert.False(string.IsNullOrEmpty(playlistItem.Snippet.Title));
                }
            }
        }
        catch (Exception ex)
        {
            Assert.True(false, "An error occurred: " + ex.Message);
        }
    }
}
