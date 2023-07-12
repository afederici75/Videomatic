using Application.Tests.Helpers;
using Ardalis.Result;
using Company.Videomatic.Application.Features.Artifacts;
using Company.Videomatic.Application.Features.Artifacts.Commands;
using Company.Videomatic.Application.Features.Artifacts.Queries;
using Company.Videomatic.Domain.Abstractions;
using Company.Videomatic.Domain.Aggregates.Artifact;
using Company.Videomatic.Domain.Aggregates.Video;
using Infrastructure.Data.Tests.Helpers;

namespace Infrastructure.Data.Tests;

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

        Fixture.SkipDeletingDatabase = true;
    }

    public DbContextFixture Fixture { get; }
    public IRepository<Artifact> Repository { get; }
    public ISender Sender { get; }

    async Task<VideoId> GenerateDummyVideoAsync([System.Runtime.CompilerServices.CallerMemberName] string callerId = "")
    {
        var createCommand = CreateVideoCommandBuilder.WithRandomValuesAndEmptyVideoDetails(callerId);

        CreateVideoResponse response = await Sender.Send(createCommand);
        return response.Id;
    }

    [Fact]
    public async Task CreateArtifact()
    {
        var videoId = await GenerateDummyVideoAsync();
        var createCommand = CreateArtifactCommandBuilder.WithDummyValues(videoId);

        Result<CreateArtifactResponse> response = await Sender.Send(createCommand);

        // Checks
        response.IsSuccess.Should().BeTrue();

        var artifact = Fixture.DbContext.Artifacts.Single(x => x.Id == response.Value.Id);

        artifact.Text.Should().Be(createCommand.Text);
        artifact.Type.Should().Be(createCommand.Type);
        artifact.VideoId.Should().Be(videoId);        
    }

    [Fact]
    public async Task DeleteArtifact()
    {
        var videoId = await GenerateDummyVideoAsync();
        var createdResponse = await Sender.Send(CreateArtifactCommandBuilder.WithDummyValues(videoId));

        // Executes
        var deletedResponse = await Sender.Send(new DeleteArtifactCommand(createdResponse.Value.Id));

        // Checks
        createdResponse.IsSuccess.Should().BeTrue();

        var row = await Fixture.DbContext.Artifacts
            .Where(x => x.Id == createdResponse.Value.Id)
            .FirstOrDefaultAsync();

        row.Should().BeNull();
    }

    [Fact]
    public async Task UpdateArtifact()
    {
        // Prepares        
        var videoId = await GenerateDummyVideoAsync();
        var response = await Sender.Send(CreateArtifactCommandBuilder.WithDummyValues(videoId));

        // Executes
        var updateCommand = new UpdateArtifactCommand(
            response.Value.Id,
            "New Name",
            "New Description");

        UpdateArtifactResponse updatedResponse = await Sender.Send(updateCommand);

        // Checks
        var video = await Fixture.DbContext.Artifacts
            .Where(x => x.Id == response.Value.Id)
            .SingleAsync();

        video.Text.Should().BeEquivalentTo(updateCommand.Text);
        video.Name.Should().BeEquivalentTo(updateCommand.Name);
    }

    [Theory]
    [InlineData(null, null, false, 4)]
    [InlineData(null, "Id DESC", false, 4)]
    [InlineData(null, "Id", false, 4)]
    [InlineData("another AI generated", "Id   ASC", false, 2)]
    [InlineData("another AI generated", "Name  DESC", false, 2)]
    //[InlineData("Philosophy", "TagCount desc, Id asc", false, 1)]
    // TODO: missing paging tests and should add more anyway
    public async Task GetArtifacts(string? searchText, string? orderBy, bool includeCounts, int expectedResults)
    {
        var query = new GetArtifactsQuery(
            SearchText: searchText,
            OrderBy: orderBy,
            Page: null, // Uses to 1 by default
            PageSize: null); // Uses 10 by default

        PageResult<ArtifactDTO> response = await Sender.Send(query);

        // Checks
        response.Count.Should().Be(expectedResults);
        response.TotalCount.Should().Be(expectedResults);

        // TODO: find a way to check the SQL uses DESC and ASC. I checked and it seems to 
        // work but it would be nice to test it here.
    }
}