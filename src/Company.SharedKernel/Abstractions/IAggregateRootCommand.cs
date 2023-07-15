namespace Company.SharedKernel.Abstractions;

public interface IAggregateRootCommand<TAggregateRoot>
    where TAggregateRoot : class, IAggregateRoot
{ }
