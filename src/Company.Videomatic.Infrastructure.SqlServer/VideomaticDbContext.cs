using Company.Videomatic.Domain;
using Microsoft.EntityFrameworkCore;

namespace Company.Videomatic.Infrastructure.SqlServer;

public partial class VideomaticDbContext : DbContext
{
    public VideomaticDbContext(DbContextOptions<VideomaticDbContext> options) 
        : base(options)
    {
    }
    
    public DbSet<Video> Videos { get; set; } = null!;
    public DbSet<Thumbnail> Thumbnails { get; set; } = null!;
    public DbSet<Folder> Folders { get; set; } = null!;
    public DbSet<Transcript> Transcripts { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.HasSequence<long>(DbConstants.SequenceName);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VideomaticDbContext).Assembly);
    }
}
