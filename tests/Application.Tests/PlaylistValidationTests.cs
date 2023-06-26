namespace Application.Tests;

/// <summary>
/// Tests that verify FluentValidation is working as expected.
/// See https://docs.fluentvalidation.net/en/latest/testing.html
/// </summary>
public class PlaylistValidationTests
{
    public IServiceProvider ServiceProvider { get; }

    public PlaylistValidationTests(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    void Validate<TVALIDATOR, TREQUEST>(TREQUEST request, int expectedErrors)
        where TVALIDATOR : IValidator<TREQUEST>
    {
        // This way if the validator's ctor has parameters they will get resolved.
        var validator = ServiceProvider.GetService<TVALIDATOR>();
        var validation = validator.TestValidate(request);
        validation.Errors.Should().HaveCount(expectedErrors);
    }

    [Theory]
    [InlineData(null, null, 1)]
    [InlineData("", null, 1)]
    [InlineData("Play list", null, 0)]
    [InlineData("Play list", "Description", 0)]
    public void T01_ValidateCreatePlaylistCommand(string name, string? description, int expectedErrors)
    {
        Validate<CreatePlaylistCommandValidator, CreatePlaylistCommand>(new (name, description), expectedErrors);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(-1, 1)]
    [InlineData(1, 0)]    
    public void T02_ValidateDeletePlaylistCommand(long id, int expectedErrors)
    {
        Validate<DeletePlaylistCommandValidator, DeletePlaylistCommand>(new(id), expectedErrors);        
    }

    [Theory]
    [InlineData(-1, null, null, 2)]
    [InlineData(-1, "", null, 2)]
    [InlineData(1, null, null, 1)]
    [InlineData(1, "Play list", null, 0)]
    [InlineData(2, "Play list", "Description", 0)]
    public void T03_ValidateUpdatePlaylistCommand(long id, string name, string? description, int expectedErrors)
    {
        Validate<UpdatePlaylistCommandValidator, UpdatePlaylistCommand>(new (id, name, description), expectedErrors);        
    }

    [Theory]
    [InlineData(null, null, null, null, false, 0)]
    [InlineData(null, null, -1, -1, false, 2)]
    [InlineData("", null, 1, 1, false, 0)]
    public void T04_ValidateGetPlaylistQuery(string? filter, string? orderBy, int? page, int? pageSize, bool includeCounts, int expectedErrors)
    {
        Validate<GetPlaylistsQueryValidator, GetPlaylistsQuery>(new(filter, orderBy, page, pageSize, includeCounts), expectedErrors);        
    }
}