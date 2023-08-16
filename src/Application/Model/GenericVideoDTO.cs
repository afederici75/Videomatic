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
    string privacyStatus) : ImportableDTOBase(providerId: ProviderId, providerItemId: ProviderItemId, etag: ETag, channelId: channelId, channelName: channelName,
                                                  name: name, description: description, publishedOn: publishedOn, thumbnail: thumbnail, picture: picture, tags: tags,
                                                  defaultLanguage: defaultLanguage, localizationInfo: localizationInfo)
{ 
    public string? EmbedHtml { get; } = embedHtml;
    public string PrivacyStatus { get; } = privacyStatus;

}


public static class GenericVideoExtensions
{
    public static EntityOrigin ToEntityOrigin(this GenericVideoDTO gv)
        => new (providerId: gv.ProviderId,
                providerItemId: gv.ProviderItemId,
                etag: gv.ETag,
                channelId: gv.ChannelId,
                channelName: gv.ChannelName,
                name: gv.Name,
                description: gv.Description,
                publishedOn: gv.PublishedAt,
                embedHtml: gv.EmbedHtml,
                defaultLanguage: gv.DefaultLanguage,
                thumbnail: gv.Thumbnail,
                picture: gv.Picture
                );

    public static Video ToVideo(this GenericVideoDTO gv)
    {
        var res = new Video(gv.Name, gv.Description);
        res.SetOrigin(gv.ToEntityOrigin());
        return res;
    }
}