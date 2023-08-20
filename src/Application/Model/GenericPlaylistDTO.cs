using SharedKernel.Model;

namespace Application.Model;

public class GenericPlaylistDTO(
    // Inherited
    string providerId,
    string providerItemId,
    string etag,
    string channelId,
    string channelName,
    string name,
    string? description,

    DateTime? publishedOn,

    ImageReference thumbnail,
    ImageReference picture,
    IEnumerable<string>? tags,

    // New
    string? embedHtml,

    string? defaultLanguage,
    NameAndDescription? localizationInfo,
    string privacyStatus,

    int videoCount) : ImportableDTOBase(
        providerId: providerId, providerItemId: providerItemId, etag: etag, channelId: channelId, channelName: channelName,
        name: name, description: description, publishedOn: publishedOn, thumbnail: thumbnail, picture: picture, tags: tags, 
        defaultLanguage: defaultLanguage, localizationInfo: localizationInfo)
{ 
    public string? EmbedHtml { get; } = embedHtml;
    public string PrivacyStatus { get; } = privacyStatus;
    public int VideoCount { get; } = videoCount;    
}
