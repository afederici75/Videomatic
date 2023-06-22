namespace VideomaticWebAPI;

public static class WebApplicationExtensions
{
    static WebApplicationBuilder MapUserEndpoints<T>(this WebApplicationBuilder builder)
        where T : IRequest<T>
    {

        return builder;
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