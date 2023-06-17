using System.ComponentModel.Design;

namespace Company.Videomatic.Infrastructure.Data.Repositories;

public class PlaylistRepository : IPlaylistRepository
{
    private readonly VideomaticDbContext _dbContext;
    private readonly IMapper _mapper;

    public PlaylistRepository(VideomaticDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Playlist> CreateAsync(Playlist playlist, CancellationToken cancellationToken)
    {
        PlaylistDb dbPlaylist = _mapper.Map<Playlist, PlaylistDb>(playlist);

        var entry = _dbContext.Add(dbPlaylist);
        var res = await _dbContext.SaveChangesAsync(cancellationToken);

        //_dbContext.ChangeTracker.Clear();

        return _mapper.Map<PlaylistDb, Playlist>(entry.Entity);
    }

    public async Task<Playlist> UpdateAsync(Playlist playlist, CancellationToken cancellationToken)
    {
        var newValue = _mapper.Map<Playlist, PlaylistDb>(playlist);

        var attached = await _dbContext.Playlists.AsTracking()
            //.Include(x => x.Videos)
            .SingleAsync(x => x.Id == playlist.Id, cancellationToken);

        var entry = _dbContext.Entry(attached);
        entry.State = EntityState.Detached;
        //foreach (var item in attached.Videos.ToList())
        //    _dbContext.Entry(item).State = EntityState.Detached;

        var entry2 = _dbContext.Attach(newValue);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<PlaylistDb, Playlist>(entry2.Entity);
    }

    public async Task<Playlist?> GetByIdAsync(PlaylistByIdQuery args, CancellationToken cancellationToken)
    {
        IQueryable<PlaylistDb> source = _dbContext.Playlists.AsNoTracking();

        if (args.Includes != null)
        {
            foreach (var include in args.Includes)
            {
                source = source.Include(include);
            }
        }        

        var dbPlaylist = await source.FirstOrDefaultAsync(p => p.Id == args.Id, cancellationToken);
        if (dbPlaylist == null)
            return null;

        var playlist = _mapper.Map<PlaylistDb, Playlist>(dbPlaylist);
        return playlist;
    }
}