using Domain.Videos;

namespace Infrastructure.Data;

public abstract class VideomaticDbContext : DbContext
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
    public DbSet<Transcript> Transcripts { get; set; } = null!;
    public DbSet<Playlist> Playlists { get; set; } = null!;
    public DbSet<Video> Videos { get; set; } = null!;
    public DbSet<PlaylistVideo> PlaylistVideos { get; set; } = null!;
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var descendantType = GetType();
        modelBuilder.ApplyConfigurationsFromAssembly(descendantType.Assembly);
    }
}
