using Domain.Videos;
using SharedKernel.Model;

namespace Application.Model;

public class GenericVideoDTO(
    // Inherited
    string ProviderId,
    string ProviderItemId,
    string ETag,
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
    NameAndDescription localizationInfo,
    string privacyStatus,
    IEnumerable<string>? topicCategories) : ImportableDTOBase(providerId: ProviderId, providerItemId: ProviderItemId, etag: ETag, channelId: channelId, channelName: channelName,
                                                  name: name, description: description, publishedOn: publishedOn, thumbnail: thumbnail, picture: picture, tags: tags,
                                                  defaultLanguage: defaultLanguage, localizationInfo: localizationInfo)
{ 
    public string? EmbedHtml { get; } = embedHtml;
    public string PrivacyStatus { get; } = privacyStatus;
    public IEnumerable<string>? TopicCategories { get; } = topicCategories;
}