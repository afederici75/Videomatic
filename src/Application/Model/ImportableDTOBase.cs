using SharedKernel.Model;

namespace Application.Model;

public class ImportableDTOBase(
    string providerId,
    string providerItemId,
    string etag,

    string channelId,
    string channelName,

    string name,
    string? description,

    DateTime? publishedOn,
    ImageReference picture,
    ImageReference thumbnail,

    IEnumerable<string>? tags,

    string? defaultLanguage,
    NameAndDescription? localizationInfo)
{ 
    public string ProviderId { get; } = providerId;
    public string ProviderItemId { get; } = providerItemId;
    public string ETag { get; } = etag;
    public string ChannelId { get; } = channelId;
    public string ChannelName { get; } = channelName;
    public string Name { get; } = name;
    public string? Description { get; } = description;
    public DateTime? PublishedAt { get; } = publishedOn;
    public ImageReference Picture { get; } = picture;
    public ImageReference Thumbnail { get; } = thumbnail;
    public IEnumerable<string>? Tags { get; } = tags;
    public string? DefaultLanguage { get; } = defaultLanguage;
    public NameAndDescription? LocalizationInfo { get; } = localizationInfo;

}
