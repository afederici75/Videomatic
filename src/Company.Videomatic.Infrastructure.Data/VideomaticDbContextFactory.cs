namespace Company.Videomatic.Infrastructure.Data;

public abstract class VideomaticDbContextFactory<TDBCONTEXTIMPL> : IDbContextFactory<TDBCONTEXTIMPL>
    where TDBCONTEXTIMPL : VideomaticDbContext
{
    public VideomaticDbContextFactory()
    {
            
    }

    public abstract TDBCONTEXTIMPL CreateDbContext();
}
