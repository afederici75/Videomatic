using Application.Tests.Helpers;
using Company.Videomatic.Application.Features.Transcripts.Commands;
using Company.Videomatic.Domain.Aggregates.Transcript;

namespace Infrastructure.Tests.Data;

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

       // Fixture.SkipDeletingDatabase = true;
    }

    public DbContextFixture Fixture { get; }
    public IRepository<Transcript> Repository { get; }
    public ISender Sender { get; }

    //[Fact]
    //public async Task CreateAndDeleteTranscript()
    //{
    //    // Creates
    //    var createCommand = CreateTranscriptCommandBuilder.WithDummyValues(1);

    //    var response = await Sender.Send(createCommand);

    //    // Checks
    //    response.IsSuccess.Should().BeTrue();
        
    //    var transcript = await Repository.GetByIdAsync(response.Value.Id);    

    //    transcript.Should().NotBeNull();
    //    transcript!.Language.Should().Be(createCommand.Language);
    //    transcript!.Lines.Should().HaveCount(createCommand.Lines.Count());
    //    transcript!.VideoId.Value.Should().Be(createCommand.VideoId);

    //    // Deletes
    //    var ok = await Sender.Send(new DeleteTranscriptCommand(createCommand.VideoId));
    //    ok.IsSuccess.Should().BeTrue();
    //}
}
