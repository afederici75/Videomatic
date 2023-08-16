using Application.Features.Artifacts.Commands;
using Domain.Videos;

namespace Application.Tests.Helpers;

public class CreateArtifactCommandBuilder
{
    public static CreateArtifactCommand WithDummyValues(
        VideoId videoId,
        string name = "ArtifactName",
        string type = "ArtifactType",
        string text = "ArtifactText")
    {
        return new CreateArtifactCommand(videoId: videoId,
                                         name: name,
                                         type: type,
                                         text: text);
    }
}
