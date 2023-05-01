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

        modelBuilder.HasSequence<long>(SequenceName);
    }
}
