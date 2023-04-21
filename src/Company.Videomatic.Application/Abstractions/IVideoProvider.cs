using Company.Videomatic.Domain;

namespace Company.Videomatic.Application.Abstractions;

public interface IVideoProvider
{
    string Name { get; }
    Task<IEnumerable<Folder>> GetRoot();
}
