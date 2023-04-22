using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Domain;

namespace Company.Videomatic.Application.Implementations;

public class InMemoryVideoStorage : IVideoStorage
{
    readonly Dictionary<int, Video> _items = new();

    public Task<bool> DeleteVideo(int id)
    {
        return Task.FromResult(_items.Remove(id));          
    }

    public Task<int> UpdateVideo(Video link)
    {
        if (link.Id <= 0)
        {
            link.Id = _items.Count + 1;
        }

        _items[link.Id] = link;
        return Task.FromResult(link.Id);
    }
}
