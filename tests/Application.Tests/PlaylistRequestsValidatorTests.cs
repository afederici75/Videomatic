namespace Application.Tests;

public class PlaylistRequestsValidatorTests
{
    public ValidatorHelper ValidatorHelper { get; }

    public PlaylistRequestsValidatorTests(IServiceProvider serviceProvider)
    {
        ValidatorHelper = new ValidatorHelper(serviceProvider);
    }
    
    [Theory]
    [InlineData(null, null, 1)]
    [InlineData("", null, 1)]
    [InlineData("Play list", null, 0)]
    [InlineData("Play list", "Description", 0)]
    public void ValidateCreatePlaylistCommand(string name, string? description, int expectedErrors)
    {
        ValidatorHelper.Validate<CreatePlaylistCommandValidator, CreatePlaylistCommand>(new (name, description), expectedErrors);
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
        ValidatorHelper.Validate<UpdatePlaylistCommandValidator, UpdatePlaylistCommand>(new (id, name, description), expectedErrors);        
    }

    [Theory]
    [InlineData(null, null, null, null, 0)]
    [InlineData(null, null, -1, -1, 2)]
    [InlineData("", null, 1, 1, 1)]
    [InlineData("filter", null, 1, 1, 0)]
    public void ValidateGetPlaylistQuery(string? filter, string? orderBy, int? page, int? pageSize, int expectedErrors)
    {
        ValidatorHelper.Validate<GetPlaylistsQueryValidator, GetPlaylistsQuery>(new(filter, orderBy, page, pageSize), expectedErrors);        
    }
}