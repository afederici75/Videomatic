namespace Infrastructure.Tests.Data;

[Collection("DbContextTests")]
public class PlaylistsTests : IClassFixture<DbContextFixture>
{
    public PlaylistsTests(
        DbContextFixture fixture,
        IRepository<Playlist> repository,
        ISender sender)
    {
        Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        Sender = sender ?? throw new ArgumentNullException(nameof(sender));
    }

    public DbContextFixture Fixture { get; }
    public IRepository<Playlist> Repository { get; }
    public ISender Sender { get; }

    [Fact]
    public async Task CreateAndDeletePlaylist()
    {
        // Creates
        var createCommand = CreatePlaylistCommandBuilder.WithDummyValues();
        var response = await Sender.Send(createCommand);

        // Checks
        response.IsSuccess.Should().BeTrue();
        response.Value.Id.Value.Should().BeGreaterThan(0);

        var playlist = await Repository.GetByIdAsync(response.Value.Id);   

        playlist.Should().BeEquivalentTo(createCommand); // Name and Description are like in command        

        // Deletes
        var ok = await Sender.Send(new DeletePlaylistCommand(response.Value.Id));        
        ok.IsSuccess.Should().Be(true);
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
        var video = await Repository.GetByIdAsync(updatedResponse.Value.Id);
        video.Should().NotBeNull();
        video!.Name.Should().BeEquivalentTo(updateCommand.Name);
        video!.Description.Should().BeEquivalentTo(updateCommand.Description);

        // Deletes
        var ok = await Sender.Send(new DeleteArtifactCommand(updatedResponse.Value.Id));
        ok.IsSuccess.Should().Be(true);
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
}