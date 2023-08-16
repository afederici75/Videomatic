namespace Domain.Tests;

/// <summary>
/// Tests that ensure domain objects are well designed.
/// </summary>
public class ArtifactTests
{
    [Fact]
    public void CreateArtifact()
    {
        var artifact = new Artifact((VideoId)1, name: "Name", type: "AI", text: "Nothing");

        artifact.Should().NotBeNull();
        artifact.Id.Should().Be((ArtifactId)0);
        artifact.VideoId.Value.Should().Be(1);
        artifact.Name.Should().Be("Name");
        artifact.Type.Should().Be("AI");
        artifact.Text.Should().Be("Nothing");
    }
}
