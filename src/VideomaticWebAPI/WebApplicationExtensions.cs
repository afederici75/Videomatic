using Application.Features.Playlists.Commands;
using Application.Features.Playlists.Queries;
using Application.Features.Transcripts.Commands;
using Application.Features.Videos.Commands;
using Application.Features.Videos.Queries;
using Microsoft.AspNetCore.Mvc;

namespace VideomaticWebAPI;

public static class WebApplicationExtensions
{
    public static WebApplication MapVideomaticEndpoints(this WebApplication app)
    {
        //app.MediateGet<GetPlaylistsQuery>("Playlists");        
        //app.MediatePost<CreatePlaylistCommand>("Playlists");
        //app.MediatePut<UpdatePlaylistCommand>("Playlists");
        //app.MediateDelete<DeletePlaylistCommand>("Playlists");
        //
        //app.MediateGet<GetVideosQuery>("Videos");
        //app.MediatePost<CreateVideoCommand>("Videos");
        //app.MediatePut<UpdateVideoCommand>("Videos");
        //app.MediateDelete<DeleteVideoCommand>("Videos");
        //app.MediatePost<SetVideoTags>("Videos");
        //app.MediatePost<LinkPlaylistToVideosCommand>("Videos");
        //app.MediatePost<ImportYoutubeVideosCommand>("Videos");

        return app;
    }

    //private static WebApplication MediateGet<T>(this WebApplication app,
    //    string template) where T : class
    //{
    //    app.MapGet(template + '/' +typeof(T).Name.Replace("Query", string.Empty), async (IMediator mediator, [AsParameters] T request) =>
    //    {
    //        var resp = await mediator.Send(request);
    //        return Results.Ok(resp);
    //    });
    //    return app;
    //}
    //
    //private static WebApplication MediatePost<T>(this WebApplication app,
    //    string template) where T : class
    //{
    //    app.MapPost(template + '/' + typeof(T).Name.Replace("Command", string.Empty), async (IMediator mediator, [FromBody] T request) =>
    //    {
    //        var resp = await mediator.Send(request);
    //        return Results.Ok(resp);
    //    })
    //        .WithTags(template);
    //    return app;
    //}
    //
    //private static WebApplication MediateDelete<T>(this WebApplication app,
    //    string template) where T : class
    //{
    //    app.MapDelete(template + '/' + typeof(T).Name.Replace("Command", string.Empty), async (IMediator mediator, [AsParameters] T request) =>
    //    {
    //        var resp = await mediator.Send(request);
    //        return Results.Ok(resp);
    //    })
    //    .WithTags(template);
    //    return app;
    //}
    //
    //private static WebApplication MediatePut<T>(this WebApplication app,
    //    string template) where T : class
    //{
    //    app.MapPut(template + '/' + typeof(T).Name.Replace("Command", string.Empty), async (IMediator mediator, [FromBody] T request) =>
    //    {
    //        var resp = await mediator.Send(request);
    //        return Results.Ok(resp);
    //    })
    //    .WithTags(template);
    //    return app;
    //}

}