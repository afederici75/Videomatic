using Company.Videomatic.Domain.Model;

namespace Company.Videomatic.Application.Abstractions;

public interface IVideoImporter
{
    public Task<Video> ImportAsync(Uri location);
}
