using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection.Metadata.Ecma335;

namespace Company.Videomatic.Infrastructure.Data.SqlServer;

public class SqlServerVideomaticDbContext : VideomaticDbContext
{
    public const string ProviderName = "SqlServer";
    public const string SequenceName = "MainId";
   
    public SqlServerVideomaticDbContext(DbContextOptions<SqlServerVideomaticDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!TryGetSequenceOfType(entityType.ClrType, out var sequenceName))
                continue;
            
            modelBuilder.HasSequence<long>(sequenceName);            
        }        
    }

    public static bool TryGetSequenceOfType(Type type, out string sequenceName)
    {
        sequenceName = default!;
        if (!typeof(IEntity).IsAssignableFrom(type))
            return false;

        sequenceName = type.Name + "Sequence";
        return true;
    }
}
