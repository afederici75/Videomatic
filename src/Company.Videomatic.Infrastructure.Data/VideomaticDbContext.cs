using Microsoft.EntityFrameworkCore.Infrastructure;

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
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
    
    public DbSet<Video> Videos { get; set; } = null!;
    public DbSet<Thumbnail> Thumbnails { get; set; } = null!;
    public DbSet<Folder> Folders { get; set; } = null!;
    public DbSet<Transcript> Transcripts { get; set; } = null!;

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
