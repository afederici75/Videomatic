using SharedKernel.Model;

namespace Application.Model;

public abstract record GenericImportable(
    string ProviderId,
    string ProviderItemId,
    string ETag,

    string ChannelId,
    string ChannelName,

    string Name,
    string? Description,

    DateTime? PublishedAt,
    ImageReference Picture,
    ImageReference Thumbnail,
    
    IEnumerable<string>? Tags);
