namespace Company.Videomatic.Domain.Abstractions;

public interface IRepository<TAGGREGATE> 
    where TAGGREGATE : IAggregateRoot
{

}
