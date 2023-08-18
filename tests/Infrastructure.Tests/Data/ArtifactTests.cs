namespace Infrastructure.Tests.Data;

[Collection("DbContextTests")]
public class ArtifactsTests : IClassFixture<DbContextFixture>
{
    public ArtifactsTests(
        DbContextFixture fixture,
        IRepository<Artifact> repository,
        ISender sender)
    {
        Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        Sender = sender ?? throw new ArgumentNullException(nameof(sender));
    }

    public DbContextFixture Fixture { get; }
    public IRepository<Artifact> Repository { get; }
    public ISender Sender { get; }

    [Fact]
    public async Task CreateAndDeleteArtifact()
    {
        // Creates
        var createCommand = CreateArtifactCommandBuilder.WithDummyValues((VideoId)1);
        var response = await Sender.Send(createCommand);

        // Checks
        response.IsSuccess.Should().BeTrue();
        
        var artifact = await Repository.GetByIdAsync(response.Value.Id);    

        artifact.Should().NotBeNull();
        artifact!.Text.Should().Be(createCommand.Text);
        artifact!.Type.Should().Be(createCommand.Type);
        artifact!.VideoId.Value.Should().Be(createCommand.VideoId);

        // Deletes
        var ok = await Sender.Send(new DeleteArtifactCommand(artifact.Id));        
        ok.IsSuccess.Should().Be(true);
    }

    [Fact]
    public async Task UpdateArtifact()
    {
        // Creates
        var createCommand = CreateArtifactCommandBuilder.WithDummyValues((VideoId)1);
        var response = await Sender.Send(createCommand);

        // Checks
        response.IsSuccess.Should().BeTrue();

        // Executes
        var updateCommand = new UpdateArtifactCommand(response.Value.Id, "New Name", "New Description");        
        Result<Artifact> updatedResponse = await Sender.Send(updateCommand);

        // Checks
        updatedResponse.IsSuccess.Should().BeTrue();

        var dbArtifact = await Repository.GetByIdAsync(updatedResponse.Value.Id);        
        
        dbArtifact.Should().NotBeNull();
        dbArtifact!.Text.Should().BeEquivalentTo(updateCommand.Text);
        dbArtifact!.Name.Should().BeEquivalentTo(updateCommand.Name);

        // Deletes
        var ok = await Sender.Send(new DeleteArtifactCommand(updatedResponse.Value.Id));
        ok.IsSuccess.Should().Be(true);
    }

    [Theory()]
    [InlineData(null, null, 4)]
    [InlineData(null, "Id DESC", 4)]
    [InlineData(null, "Id", 4)]
    [InlineData("#shivavideo", "Id   ASC", 2)]
    [InlineData("#IfRealityVideo", "Name  DESC", 2)]
    //[InlineData("Philosophy", "TagCount desc, Id asc", false, 1)]
    // TODO: missing paging tests and should add more anyway
    public async Task GetArtifacts(string? searchText, string? orderBy, int expectedResults)
    {

        var query = new GetArtifactsQuery(searchText: searchText, orderBy: orderBy);
        Page<ArtifactDTO> response = await Sender.Send(query);

        // Checks
        response.Count.Should().Be(expectedResults);
        response.TotalCount.Should().Be(expectedResults);        
    }
}