using Domain.Videos;
using Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
            if (entry.Entity is TrackedEntity trackable)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        // Marks properties as "don't touch": we don't want to update on a Modify operation
                        entry.Property(nameof(TrackedEntity.CreatedOn)).IsModified = false;
                        entry.Property(nameof(TrackedEntity.CreatedBy)).IsModified = false;

                        // Sets CreatedBy/CreatedOn for any updated entity 
                        var cv = entry.Property(nameof(TrackedEntity.UpdatedOn)).CurrentValue;
                        entry.Property(nameof(TrackedEntity.UpdatedOn)).CurrentValue = utcNow;
                        var cv2 = entry.Property(nameof(TrackedEntity.UpdatedOn)).CurrentValue;

                        entry.Property(nameof(TrackedEntity.UpdatedBy)).CurrentValue = "UPDATE_ID2";
                        break;

                    case EntityState.Added:
                        // Sets CreatedBy/CreatedOn for any new entities
                        entry.Property(nameof(TrackedEntity.CreatedOn)).CurrentValue = utcNow;
                        entry.Property(nameof(TrackedEntity.CreatedBy)).CurrentValue = "CREATE_ID2";

                        break;
                }
            }
        }
    }
}
