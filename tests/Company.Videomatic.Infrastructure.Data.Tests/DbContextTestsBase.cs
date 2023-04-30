namespace Company.Videomatic.Infrastructure.Data.Tests;

[Collection("DbContext")]
public abstract class DbContextTestsBase<TDBContext>
    where TDBContext : VideomaticDbContext
{
    protected DbContextTestsBase(DbContextFixture<TDBContext> fixture)
    {
        Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        //Fixture.SkipDeletingDatabase();
    }

    public DbContextFixture<TDBContext> Fixture { get; }
}


