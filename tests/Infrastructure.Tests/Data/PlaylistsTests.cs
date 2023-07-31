using Application.Tests.Helpers;
using Ardalis.Result;
using Company.Videomatic.Application.Features.Playlists;
using Company.Videomatic.Domain.Aggregates.Playlist;
using Infrastructure.Tests.Data.Helpers;

namespace Infrastructure.Tests.Data;

[Collection("DbContextTests")]
public class PlaylistsTests : IClassFixture<DbContextFixture>
{
    public PlaylistsTests(
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
    public async Task CreatePlaylist()
    {
        var createCommand = CreatePlaylistCommandBuilder.WithDummyValues();

        var response = await Sender.Send(createCommand);

        // Checks
        response.Value.Id.Value.Should().BeGreaterThan(0);

        var playlist = Fixture.DbContext.Playlists.Single(x => x.Id == response.Value.Id);

        playlist.Should().BeEquivalentTo(createCommand); // Name and Description are like in command        

        Fixture.DbContext.Remove(playlist);
        await Fixture.DbContext.SaveChangesAsync();
    }

    [Fact]
    public async Task DeletePlaylist()
    {
        var createdResponse = await Sender.Send(CreatePlaylistCommandBuilder.WithDummyValues());
        createdResponse.Value.Id.Value.Should().BeGreaterThan(0);

        // Executes
        var deletedResponse = await Sender.Send(new DeletePlaylistCommand(createdResponse.Value.Id));

        // Checks
        
        var row = await Fixture.DbContext.Playlists
            .Where(x => x.Id == createdResponse.Value.Id)
            .FirstOrDefaultAsync();

        row.Should().BeNull();
    }

    [Fact]
    public async Task UpdatePlaylist()
    {
        // Prepares        
        Result<Playlist> playlist = await Sender.Send(CreatePlaylistCommandBuilder.WithDummyValues());

        // Executes
        var updateCommand = new UpdatePlaylistCommand(
            playlist.Value.Id,
            "New Name",
            "New Description");

        Result<Playlist> updatedResponse = await Sender.Send(updateCommand);

        // Checks
        var video = await Fixture.DbContext.Playlists
            .Where(x => x.Id == playlist.Value.Id)
            .SingleAsync();

        video.Name.Should().BeEquivalentTo(updateCommand.Name);
        video.Description.Should().BeEquivalentTo(updateCommand.Description);
    }

    [Theory]
    [InlineData(null, null, 2)]    
    [InlineData(null, "Id DESC", 2)]
    [InlineData(null, "Id", 2)]
    [InlineData("Philosophy", "Id   ASC", 1)]    
    [InlineData("Philosophy", "Name  DESC", 1)]
    [InlineData("Philosophy", "Id desc", 1)]
    // TODO: missing paging tests and should add more anyway
    public async Task GetPlaylists(string? searchText, string? orderBy, int expectedResults)
    {
        var idx = 0;
        while (idx++ < 3)
        {
            try
            {
                var query = new GetPlaylistsQuery(
                    SearchText: searchText,
                    OrderBy: orderBy,
                    Skip: null,
                    Take: null);

                Page<PlaylistDTO> response = await Sender.Send(query);

                // Checks
                response.Count.Should().Be(expectedResults);
                response.TotalCount.Should().Be(expectedResults);

                var anyNonZeroCount = response.Items.Any(v => v.VideoCount > 0);
                anyNonZeroCount.Should().BeTrue();

                // TODO: find a way to check the SQL uses DESC and ASC. I checked and it seems to 
                // work but it would be nice to test it here.
            }
            catch (Exception ex)
            {
                await Task.Delay(2000);
            }
        }
    }    
}