namespace Infrastructure.Data;

public abstract class VideoMaticDbContextFactory<TDBCONTEXT> : IDbContextFactory<VideomaticDbContext>
    where TDBCONTEXT : VideomaticDbContext
{    
    public VideoMaticDbContextFactory(IConfiguration configuration)
    {
        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public IConfiguration Configuration { get; }

    protected virtual string GetConnectionString()
    {
        // Looks for the connection string <ProviderName> [e.g. SqlServer, Sqlite, etc.]
        // Provider name is the prefix of the name of the DbContext class, e.g. **SqlServer**VideomaticDbContext
        var dbCtxNamePrefix = typeof(TDBCONTEXT).Name.Replace(nameof(VideomaticDbContext), string.Empty);
        var connectionName = $"{dbCtxNamePrefix}";
        var connString = Configuration.GetConnectionString(connectionName) ?? throw new Exception($"Required connection string '{connectionName}' missing.");

        return connString;
    }

    protected virtual void ConfigureContext(string connectionString, DbContextOptionsBuilder builder)
    {
        builder.EnableSensitiveDataLogging();
    }

    public VideomaticDbContext CreateDbContext()
    {
        var connString = GetConnectionString();

        var optionsBuilder = new DbContextOptionsBuilder();
        ConfigureContext(connString, optionsBuilder);
        
        return (VideomaticDbContext)Activator.CreateInstance(
            typeof(TDBCONTEXT), 
            optionsBuilder.Options)!;
    }
}
