using Company.Videomatic.Application.Features.Playlists.Queries;
using Microsoft.AspNetCore.Mvc;

namespace VideomaticWebAPI;

public static class WebApplicationExtensions
{
    public static WebApplication MapVideomaticEndpoints(this WebApplication app)
    {
        //app.MediatePost<GetPlaylistsQuery>("Videos");
        app.MapPost("Videos", async (IMediator mediator, [FromBody] GetPlaylistsQuery request) =>
        {
            var resp = await mediator.Send(request);
            return Results.Ok(resp);
        });
        return app;
    }

    private static WebApplication MediateGet<T>(this WebApplication app,
        string template) where T : class
    {
        app.MapGet(template, async (IMediator mediator, [FromQuery] T request) =>
        {
            var resp = await mediator.Send(request);
            return Results.Ok(resp);
        });
        return app;
    }

    private static WebApplication MediatePost<T>(this WebApplication app,
        string template) where T : class
    {
        app.MapPost(template, async (IMediator mediator, [FromBody] T request) =>
        {
            var resp = await mediator.Send(request);
            return Results.Ok(resp);
        });
        return app;
    }

    //app.MapGet("/videos/" + nameof(GetVideosDTOQuery), 
    //    async ([AsParameters] GetVideosDTOQuery query,
    //           ISender sender) => 
    //    {
    //        var resp = await sender.Send(query);
    //        return Results.Ok(resp);
    //    }).res;
    //app.MapGet("/videos/" + nameof(GetVideosDTOQuery), GetVideosDTOQuery);

    //app.MapGet("/videos/" + nameof(GetTranscriptQuery),
    //    async ([AsParameters] GetTranscriptQuery query,
    //           ISender sender) =>
    //    {
    //        var resp = await sender.Send(query);
    //        return Results.Ok(resp);
    //    });


    //app.MapPost("videos/ImportVideoCommand",
    //    async (ImportVideoCommand command,
    //           ISender sender) =>
    //    {
    //        var resp = await sender.Send(command);
    //        return Results.Ok(resp);
    //    });

    //app.MapPut("videos/" + nameof(UpdateVideoCommand),
    //    async (UpdateVideoCommand command,
    //           ISender sender) =>
    //    {
    //        var resp = await sender.Send(command);
    //        return Results.Ok(resp);
    //    });

    //app.MapDelete("videos/" + nameof(DeleteVideoCommand),
    //    async ([AsParameters] ImportVideoCommand command,
    //           ISender sender) =>
    //    {
    //        var resp = await sender.Send(command);
    //        return Results.Ok(resp);
    //    });

}