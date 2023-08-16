using SharedKernel.Model;
using System.Threading.Channels;

namespace Application.Model;

public class GenericChannelDTO(
    // Inherited
    string providerId,
    string providerItemId,
    string etag,

    string name,
    string? description,

    DateTime? publishedOn,

    ImageReference thumbnail,
    ImageReference picture,    

    IEnumerable<string>? tags,
    // New
    string? defaultLanguage,
    NameAndDescription? localizationInfo,

    string? owner,
    DateTime? timeCreated,

    IEnumerable<string> topicIds,
    IEnumerable<string> topicCategories,

    ulong? videoCount,
    ulong? suscriberCount,
    ulong? viewCount) : ImportableDTOBase(
        providerId: providerId, providerItemId: providerItemId, etag: etag, channelId: providerItemId, channelName: name,
        name: name, description: description, publishedOn: publishedOn, thumbnail: thumbnail, picture: picture, tags: tags,
        defaultLanguage: defaultLanguage, localizationInfo: localizationInfo)
{ 
    public string? Owner { get; } = owner;
    public DateTime? TimeCreated { get; } = timeCreated;
    public IEnumerable<string> TopicIds { get; } = topicIds;
    public IEnumerable<string> TopicCategories { get; } = topicCategories;
    public ulong? VideoCount { get; } = videoCount;
    public ulong? SuscriberCount { get; } = suscriberCount;
    public ulong? ViewCount { get; } = viewCount;

}