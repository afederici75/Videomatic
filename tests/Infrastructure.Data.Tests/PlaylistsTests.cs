using Company.Videomatic.Domain.Abstractions;

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
        var createCommand = CreatePlaylistCommandBuilder.WithDummyValues();

        CreatedResponse response = await Sender.Send(createCommand);

        // Checks
        response.Id.Should().BeGreaterThan(0);

        var playlist = Fixture.DbContext.Playlists.Single(x => x.Id == response.Id);

        playlist.Should().BeEquivalentTo(createCommand); // Name and Description are like in command        
    }

    [Fact]
    public async Task DeletePlaylist()
    {
        var createdResponse = await Sender.Send(CreatePlaylistCommandBuilder.WithDummyValues());

        // Executes
        var deletedResponse = await Sender.Send(new DeletePlaylistCommand(createdResponse.Id));

        // Checks
        createdResponse.Id.Should().BeGreaterThan(0);

        var row = await Fixture.DbContext.Playlists
            .Where(x => x.Id == createdResponse.Id)
            .FirstOrDefaultAsync();

        row.Should().BeNull();
    }

    [Fact]
    public async Task UpdatePlaylist()
    {
        // Prepares        
        var response = await Sender.Send(CreatePlaylistCommandBuilder.WithDummyValues());

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

        video.Name.Should().BeEquivalentTo(updateCommand.Name);
        video.Description.Should().BeEquivalentTo(updateCommand.Description);
    }

    [Theory]
    [InlineData(null, null, false, 2)]
    [InlineData(null, "Id DESC", false, 2)]
    [InlineData(null, "Id", false, 2)]
    [InlineData("Philosophy", "Id   ASC", false, 1)]
    [InlineData("Philosophy", "Name  DESC", false, 1)]
    //[InlineData("Philosophy", "TagCount desc, Id asc", false, 1)]
    // TODO: missing paging tests and should add more anyway
    public async Task GetPlaylists(string? searchText, string? orderBy, bool includeCounts, int expectedResults)
    {
        var query = new GetPlaylistsQuery(
            SearchText: searchText,
            OrderBy: orderBy,
            Page: null, // Uses to 1 by default
            PageSize: null, // Uses 10 by default
            IncludeCounts: includeCounts);

        PageResult<PlaylistDTO> response = await Sender.Send(query);

        // Checks
        response.Count.Should().Be(expectedResults);
        response.TotalCount.Should().Be(expectedResults);

        // TODO: find a way to check the SQL uses DESC and ASC. I checked and it seems to 
        // work but it would be nice to test it here.
    }    
}