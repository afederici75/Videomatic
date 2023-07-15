using Application.Tests.Helpers;
using Company.Videomatic.Application.Features.Transcripts.Commands;
using Company.Videomatic.Domain.Abstractions;
using Company.Videomatic.Domain.Aggregates.Transcript;
using Infrastructure.Data.Tests.Helpers;

namespace Infrastructure.Data.Tests;

[Collection("DbContextTests")] 
public class TranscriptTests : IClassFixture<DbContextFixture>
{
    public TranscriptTests(
        DbContextFixture fixture,
        IRepository<Transcript> repository,
        ISender sender)
    {
        Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        Sender = sender ?? throw new ArgumentNullException(nameof(sender));

        Fixture.SkipDeletingDatabase = true;
    }

    public DbContextFixture Fixture { get; }
    public IRepository<Transcript> Repository { get; }
    public ISender Sender { get; }

    async Task<long> GenerateDummyVideoAsync([System.Runtime.CompilerServices.CallerMemberName] string callerId = "")
    {
        var createCommand = CreateVideoCommandBuilder.WithRandomValuesAndEmptyVideoDetails(callerId);

        var response = await Sender.Send(createCommand);
        return response.Value.Id;
    }

    [Fact]
    public async Task CreateTranscript()
    {
        var videoId = await GenerateDummyVideoAsync();
        var createCommand = CreateTranscriptCommandBuilder.WithDummyValues(videoId);

        var response = await Sender.Send(createCommand);

        // Checks
        response.Value.Id.Value.Should().BeGreaterThan(0);

        var transcript = Fixture.DbContext.            
            Transcripts.
            Single(x => x.Id == response.Value.Id);

        transcript.Language.Should().Be(createCommand.Language);
        transcript.Lines.Should().HaveCount(createCommand.Lines.Count());
        transcript.VideoId.Value.Should().Be(videoId);
    }

    [Fact]
    public async Task DeleteTranscript()
    {
        var videoId = await GenerateDummyVideoAsync();
        var createdResponse = await Sender.Send(CreateTranscriptCommandBuilder.WithDummyValues(videoId));

        // Executes
        var deletedResponse = await Sender.Send(new DeleteTranscriptCommand(createdResponse.Value.Id));

        // Checks
        createdResponse.Value.Id.Value.Should().BeGreaterThan(0);

        var row = await Fixture.DbContext.Transcripts
            .Where(x => x.Id == createdResponse.Value.Id)
            .FirstOrDefaultAsync();

        row.Should().BeNull();
    }
}
