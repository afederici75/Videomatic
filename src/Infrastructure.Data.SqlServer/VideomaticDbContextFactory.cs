using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.DependencyInjection;

public abstract class VideomaticDbContextFactory<TDBCONTEXT> : IDbContextFactory<TDBCONTEXT>
    where TDBCONTEXT : VideomaticDbContext
{
    public VideomaticDbContextFactory(IConfiguration configuration, ILoggerFactory loggerFactory)
    {
        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        LoggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
    }
    
    public IConfiguration Configuration { get; }
    public ILoggerFactory LoggerFactory { get; }

    public TDBCONTEXT CreateDbContext()
    {
        var builder = new DbContextOptionsBuilder();
        ConfigureOptions(builder, Configuration);

        return (TDBCONTEXT)Activator.CreateInstance(typeof(TDBCONTEXT), builder.Options, LoggerFactory)!;
    }

    protected abstract void ConfigureOptions(DbContextOptionsBuilder builder, IConfiguration configuration);
}