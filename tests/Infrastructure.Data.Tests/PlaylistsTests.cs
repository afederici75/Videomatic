namespace Infrastructure.Data.Tests;

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
        var command = new CreatePlaylistCommand(
            Name: nameof(CreatePlaylist) , 
            Description: $"A test playlist.");

        CreatedResponse response = await Sender.Send(command);

        // Checks
        response.Id.Should().BeGreaterThan(0);

        var playlist = Fixture.DbContext.Playlists.Single(x => x.Id == response.Id);

        playlist.Should().BeEquivalentTo(command);
    }

    [Fact]
    public async Task DeletePlaylist()
    {
        var command = new CreatePlaylistCommand(
            Name: nameof(DeletePlaylist),
            Description: "A description");

        CreatedResponse response = await Sender.Send(command);

        // Checks
        response.Id.Should().BeGreaterThan(0);

        var cnt = await Fixture.DbContext.Playlists
            .Where(x => x.Id == response.Id)
            .ExecuteDeleteAsync();

        cnt.Should().Be(1);
    }

    [Fact]
    public async Task UpdatePlaylist()
    {
        // Prepares
        var command = new CreatePlaylistCommand(
            Name: nameof(UpdatePlaylist),
            Description: "A description");

        CreatedResponse response = await Sender.Send(command);

        // Executes
        var updateCommand = new UpdatePlaylistCommand(
            response.Id,
            "New Name",
            "New Description");

        UpdatedResponse updatedResponse = await Sender.Send(updateCommand);

        // Checks
        var video = await Fixture.DbContext.Playlists
            .Where(x => x.Id == response.Id)
            .SingleAsync();

        video.Should().BeEquivalentTo(updateCommand);
    }

    [Theory]
    [InlineData(null, null, 2)]
    [InlineData(null, "Id DESC", 2)]
    [InlineData(null, "Id", 2)]
    [InlineData("Philosophy", "Id   ASC", 1)]
    [InlineData("Philosophy", "Name  DESC", 1)]
    [InlineData("Philosophy", "VideoCount desc, Id asc", 1)]
    public async Task GetPlaylists(string searchText, string orderBy, int expectedResults)
    {
        var query = new GetPlaylistsQuery(
            SearchText: searchText,
            OrderBy: orderBy);

        PageResult<PlaylistDTO> response = await Sender.Send(query);

        // Checks
        response.Count.Should().Be(expectedResults);
        response.TotalCount.Should().Be(expectedResults);

        // TODO: find a way to check the SQL uses DESC and ASC. I checked and it seems to 
        // work but it would be nice to test it here.
    }
}