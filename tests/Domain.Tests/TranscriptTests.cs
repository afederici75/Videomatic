using Company.Videomatic.Domain.Aggregates.Transcript;

namespace Domain.Tests;

/// <summary>
/// Tests that ensure domain objects are well designed.
/// </summary>
public class TranscriptTests
{ 
    [Fact]
    public void CreateTranscriptWithLines()
    {
        var transcript = new Transcript(1, "EN", new[] 
        { 
            "First line",
            "Second line"
        });
        transcript.AddLine(text: "This is a line with nothing");
        transcript.AddLine(text: "This is a line with something", startsAt: TimeSpan.FromSeconds(2), duration: TimeSpan.FromSeconds(2));

        transcript.Should().NotBeNull();
        transcript.Id.Should().BeNull();
        transcript.VideoId.Value.Should().Be(1);
        transcript.Language.Should().Be("EN");
        transcript.Lines.Should().HaveCount(4);
        transcript.Lines.Where(l => l.StartsAt is not null).Should().HaveCount(1);
        transcript.Lines.Where(l => l.Duration is null).Should().HaveCount(3);
    }

    [Fact]
    public void CreateEmptyTranscriptAndAddLines()
    {
        var transcript = new Transcript(1, "EN");

        transcript.Should().NotBeNull();
        transcript.Lines.Should().BeEmpty();

        transcript.AddLine(text: "This is a line with nothing");
        transcript.AddLine(text: "This is another line with nothing");
        transcript.AddLine(text: "This is a line with something", TimeSpan.FromDays(0), TimeSpan.FromDays(1));

        transcript.Lines.Should().HaveCount(3);
        transcript.Lines.Where(l => l.Duration is not null).Should().HaveCount(1);
        transcript.Lines.Where(l => l.StartsAt is not null).Should().HaveCount(1);
    }

}