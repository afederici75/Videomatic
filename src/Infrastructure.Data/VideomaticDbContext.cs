using Domain.Videos;
using Infrastructure.Data.Extensions;
using SharedKernel.Model;

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

        modelBuilder.AddStronglyTypedIdValueConverters(typeof(VideomaticDbContext).Assembly);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSaving();

        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        OnBeforeSaving();

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void OnBeforeSaving()
    {
        var entries = ChangeTracker.Entries();
        var utcNow = DateTime.UtcNow;

        foreach (var entry in entries)
        {
            if (entry.Entity is TrackableEntity trackable)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        // set the updated date to "now"
                        //updateable.SetUpdatedOn(utcNow);

                        // mark property as "don't touch"
                        // we don't want to update on a Modify operation
                        entry.Property(nameof(TrackableEntity.CreatedOn)).IsModified = false;
                        break;

                    case EntityState.Added:
                        // set both updated and created date to "now"
                        //updateable.SetCreatedOn(utcNow);// = utcNow;
                        //updateable.SetUpdatedOn(utcNow);// = utcNow;
                        break;
                }
            }
        }
    }
}
