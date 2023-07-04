namespace Company.Videomatic.Application.Abstractions;

public interface ICommandWithEntityId
{
    long Id { get; }
}
