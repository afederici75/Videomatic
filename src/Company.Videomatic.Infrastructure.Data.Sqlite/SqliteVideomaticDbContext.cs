using Microsoft.EntityFrameworkCore;

namespace Company.Videomatic.Infrastructure.Data.Sqlite;

public class SqliteVideomaticDbContext : VideomaticDbContext
{
    public const string ProviderName = "Sqlite";

    public SqliteVideomaticDbContext(DbContextOptions<SqliteVideomaticDbContext> options) 
        : base(options)
    {
    }
}
