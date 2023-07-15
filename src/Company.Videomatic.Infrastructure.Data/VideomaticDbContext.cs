using Company.Videomatic.Domain.Aggregates.Artifact;
using Company.Videomatic.Domain.Aggregates.Transcript;
using Company.Videomatic.Infrastructure.Data.Configurations;

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
    public DbSet<Transcript> Transcripts { get; set; } = null!;
    public DbSet<Playlist> Playlists { get; set; } = null!;
    public DbSet<Video> Videos { get; set; } = null!;
    public DbSet<PlaylistVideo> PlaylistVideos { get; set; } = null!;

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

    [Obsolete]
    public override int SaveChanges()
    {
        throw new InvalidOperationException("Use SaveChangesAsync instead.");
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        var result = base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        //ChangeTracker.Clear();
        return result;
    }

    [Obsolete]
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        throw new InvalidOperationException("Use SaveChangesAsync instead.");
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }
}
