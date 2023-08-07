using Company.Videomatic.Application.Abstractions;
using Hangfire;

namespace Company.Videomatic.Application.Handlers.Playlists.Commands;

public class ImportYoutubePlaylistsHandler : IRequestHandler<ImportYoutubePlaylistsCommand, ImportYoutubePlaylistsResponse>
{
    readonly IRepository<Playlist> Repository;
    readonly IYouTubeImporter Importer;
    readonly IYouTubeHelper Helper;
    readonly IMapper Mapper;
    readonly IPlaylistService PlaylistService;
    private readonly ISender Sender;
    readonly IBackgroundJobClient JobClient;


    public ImportYoutubePlaylistsHandler(
        IBackgroundJobClient jobClient, 
        IRepository<Playlist> repository, 
        IYouTubeImporter youTubeImporter, 
        IYouTubeHelper youTubeHelper,
        IMapper mapper, 
        IPlaylistService playlistService,
        ISender sender)
    {
        JobClient = jobClient ?? throw new ArgumentNullException(nameof(jobClient));
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        Importer = youTubeImporter ?? throw new ArgumentNullException(nameof(youTubeImporter));
        Helper = youTubeHelper ?? throw new ArgumentNullException(nameof(youTubeHelper));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        PlaylistService = playlistService ?? throw new ArgumentNullException(nameof(playlistService));
        Sender = sender ?? throw new ArgumentNullException(nameof(sender));
    }

    public Task<ImportYoutubePlaylistsResponse> Handle(ImportYoutubePlaylistsCommand request, CancellationToken cancellationToken)
    {
        var jobIds = new List<string>();
        foreach (var id in request.Urls)
        {
            var jobId = JobClient.Enqueue<ImportYoutubePlaylistsHandler>(x => x.ImportPlaylistJob(id));
            jobIds.Add(jobId);
        }        

        return Task.FromResult(new ImportYoutubePlaylistsResponse(true, jobIds));
    }

    public async Task ImportPlaylistJob(string playlistId)
    {        
        var videoIds = await Helper.GetPlaylistVideoIds(playlistId);
        var playlistInfo = await Helper.GetPlaylistInformation(playlistId);

        var newPlaylist = Playlist.Create(
            name: $"{playlistInfo.Name} (Imported on {DateTime.Now})",
            description: playlistInfo.Description);

        var storedPlaylist = await Repository.AddAsync(newPlaylist);

        var req = new ImportYoutubeVideosCommand(videoIds, storedPlaylist.Id);
        await Sender.Send(req);
    }
}