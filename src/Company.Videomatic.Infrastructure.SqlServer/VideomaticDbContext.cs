using Company.Videomatic.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        if (!(Database is DatabaseFacade))
        {
            var pn = Database.ProviderName;
        }

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        if (!(Database is DatabaseFacade))
        {
            var pn = Database.ProviderName;
        }

        //modelBuilder.HasSequence<long>(DbConstants.SequenceName);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VideomaticDbContext).Assembly);
    }
}
