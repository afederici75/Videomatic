using Company.Videomatic.Application.Features.Artifacts.Commands;
using Company.Videomatic.Domain.Video;

namespace Application.Tests.Helpers;

public class CreateArtifactCommandBuilder
{
    public static CreateArtifactCommand WithDummyValues(
        VideoId videoId,
        string name = "ArtifactName",
        string type = "ArtifactType",
        string text = "ArtifactText")
    {
        return new CreateArtifactCommand(VideoId: videoId,
                                         Name: name,
                                         Type: type,
                                         Text: text);
    }
}
