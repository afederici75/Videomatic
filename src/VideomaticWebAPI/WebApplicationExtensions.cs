using Company.Videomatic.Application.Features.DataAccess;
using Company.Videomatic.Application.Features.Playlists.Commands;
using Company.Videomatic.Application.Features.Playlists.Queries;
using Company.Videomatic.Application.Features.Videos.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Identity.Client;

namespace VideomaticWebAPI;

public static class WebApplicationExtensions
{
    public record PersonName(string FirstName, string LastName) 
    {
        //public static bool TryParse(string value, out PersonName personName)
        //{
        //    personName = new PersonName(value, value);
        //    return true;
        //}
    }

    public record TestParams(string? SearchText, string? OrderBy, int? page, int? pageSize)
    {
        //public static bool TryParse(string value, out TestParams pars)
        //{
        //    pars = new TestParams(new PersonName("a", "b"));
        //    return true;
        //}
    }

    public record TestParamsExtra(string? SearchText, string? OrderBy, int? page, int? pageSize, bool? IncludeCounts) 
        : TestParams(SearchText, OrderBy, page, pageSize);

    public static WebApplication MapVideomaticEndpoints(this WebApplication app)
    {
        app.MapGet("test", ([AsParameters] TestParamsExtra testParams) =>
        {
            return Results.Ok(testParams);
        });

        app.MediatePost<CreatePlaylistCommand>("Playlists");
        app.MediatePut<UpdatePlaylistCommand>("Playlists");
        app.MediateDelete<DeletePlaylistCommand>("Playlists");

        app.MediatePost<CreateVideoCommand>("Videos");
        app.MediatePut<UpdateVideoCommand>("Videos");
        app.MediateDelete<DeleteVideoCommand>("Videos");

        app.MediatePost<AddTagsToVideoCommand>("Videos");
        app.MediatePost<AddThumnbailsToVideoCommand>("Videos");
        app.MediatePost<AddTranscriptsToVideoCommand>("Videos");
        app.MediatePost<LinkVideosToPlaylistsCommand>("Videos");
        app.MediatePost<ImportVideoCommand>("Videos");

        return app;
    }

    private static WebApplication MediateGet<T>(this WebApplication app,
        string template) where T : class
    {
        app.MapGet(template + '/' +typeof(T).Name.Replace("Query", string.Empty), async (IMediator mediator, [AsParameters] T request) =>
        {
            var resp = await mediator.Send(request);
            return Results.Ok(resp);
        });
        return app;
    }

    private static WebApplication MediatePost<T>(this WebApplication app,
        string template) where T : class
    {
        app.MapPost(template + '/' + typeof(T).Name.Replace("Command", string.Empty), async (IMediator mediator, [FromBody] T request) =>
        {
            var resp = await mediator.Send(request);
            return Results.Ok(resp);
        })
            .WithTags(template);
        return app;
    }

    private static WebApplication MediateDelete<T>(this WebApplication app,
        string template) where T : class
    {
        app.MapDelete(template + '/' + typeof(T).Name.Replace("Command", string.Empty), async (IMediator mediator, [AsParameters] T request) =>
        {
            var resp = await mediator.Send(request);
            return Results.Ok(resp);
        })
        .WithTags(template);
        return app;
    }

    private static WebApplication MediatePut<T>(this WebApplication app,
        string template) where T : class
    {
        app.MapPut(template + '/' + typeof(T).Name.Replace("Command", string.Empty), async (IMediator mediator, [FromBody] T request) =>
        {
            var resp = await mediator.Send(request);
            return Results.Ok(resp);
        })
        .WithTags(template);
        return app;
    }

}