namespace Company.Videomatic.Infrastructure.Data;

public class VideomaticDbContext : DbContext
{
    public VideomaticDbContext()
        : base()
    {
        
    }

    public VideomaticDbContext(DbContextOptions options) 
        : base(options)
    {        
    }
    
    public DbSet<Artifact> Artifacts { get; set; } = null!;
    public DbSet<Playlist> Playlists { get; set; } = null!;
    public DbSet<PlaylistVideo> PlaylistVideos { get; set; } = null!;
    public DbSet<Thumbnail> Thumbnails { get; set; } = null!;
    public DbSet<TranscriptLine> TranscriptLines { get; set; } = null!;
    public DbSet<Transcript> Transcripts { get; set; } = null!;
    public DbSet<Video> Videos { get; set; } = null!;
    public DbSet<VideoTag> VideoTags { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);        
        
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    public async Task<int> CommitChangesAsync(CancellationToken cancellationToken)
    {
        var res = await SaveChangesAsync(cancellationToken);
        ChangeTracker.Clear();
        return res;
    }
}
