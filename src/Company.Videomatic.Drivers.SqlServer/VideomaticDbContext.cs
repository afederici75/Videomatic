using Microsoft.EntityFrameworkCore;

namespace Company.Videomatic.Drivers.SqlServer;

public class VideomaticDbContext : DbContext
{
    public class Constants
    { 
        public const string SequenceName = "IdSequence";
    }

    public VideomaticDbContext(DbContextOptions<VideomaticDbContext> options) 
        : base(options)
    {
    }
    
    //public DbSet<Video> Videos { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.HasSequence<long>(Constants.SequenceName);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VideomaticDbContext).Assembly);
    }
}
