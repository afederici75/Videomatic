using Company.Videomatic.Infrastructure.Data.Model;

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
    
    public DbSet<ArtifactDb> Artifacts { get; set; } = null!;
    public DbSet<PlaylistDb> Playlists { get; set; } = null!;
    public DbSet<PlaylistDbVideoDb> PlaylistVideos { get; set; } = null!;
    public DbSet<TagDb> Tags { get; set; } = null!;
    public DbSet<ThumbnailDb> Thumbnails { get; set; } = null!;
    public DbSet<TranscriptLineDb> TranscriptLines { get; set; } = null!;
    public DbSet<TranscriptDb> Transcripts { get; set; } = null!;
    public DbSet<VideoDb> Videos { get; set; } = null!;
    public DbSet<VideoDbTagDb> VideoTags { get; set; } = null!;

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
}
