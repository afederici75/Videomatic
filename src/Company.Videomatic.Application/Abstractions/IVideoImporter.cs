using Company.Videomatic.Domain;

namespace Company.Videomatic.Application.Abstractions;

public interface IVideoImporter
{
    public Task<VideoLink> Import(Uri location);
}
