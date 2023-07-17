using Application.Tests.Helpers;
using Company.Videomatic.Application.Features.Artifacts.Commands;
using Microsoft.Extensions.Configuration;

namespace Application.Tests.Validation;

public class ArtifactValidatorTests
{
    public ValidatorHelper ValidatorHelper { get; }

    public ArtifactValidatorTests(IServiceProvider serviceProvider, IConfiguration xyz)
    {
        ValidatorHelper = new ValidatorHelper(serviceProvider);

        var cfg = serviceProvider.GetRequiredService<Microsoft.Extensions.Configuration.IConfiguration>();
        var tmp = cfg["Application::test"];
    }


    [Theory]
    [InlineData(1, "AI", "Name", "Text", 0)]
    [InlineData(0, "AI", "Name", "Text", 1)]
    [InlineData(3, null, "Name", "Text", 1)]
    [InlineData(4, "AI", null, "Text", 1)]
    [InlineData(5, null, null, null, 3)]
    public void ValidateCreateArtifactCommand(long videoId, string type, string name, string? text, int expectedErrors)
    {
        ValidatorHelper.Validate<CreateArtifactCommandValidator, CreateArtifactCommand>(new(videoId, name, type, text), expectedErrors);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(-1, 1)]
    [InlineData(1, 0)]
    public void ValidateDeletePlaylistCommand(long id, int expectedErrors)
    {
        ValidatorHelper.Validate<DeletePlaylistCommandValidator, DeletePlaylistCommand>(new(id), expectedErrors);
    }

    [Theory]
    [InlineData(-1, null, null, 2)]
    [InlineData(-1, "", null, 2)]
    [InlineData(1, null, null, 1)]
    [InlineData(1, "Play list", null, 0)]
    [InlineData(2, "Play list", "Description", 0)]
    public void ValidateUpdatePlaylistCommand(long id, string name, string? description, int expectedErrors)
    {
        ValidatorHelper.Validate<UpdatePlaylistCommandValidator, UpdatePlaylistCommand>(new(id, name, description), expectedErrors);
    }
}
