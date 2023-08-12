using Company.Videomatic.Domain.Aggregates;

namespace Company.Videomatic.Application.Model;

public record GenericPlaylist(
    // Inherited
    string ProviderId,
    string ProviderItemId,
    string ETag,
    string ChannelId,
    string ChannelName,
    string Name,
    string? Description,

    DateTime? PublishedAt,

    Thumbnail Thumbnail,
    Thumbnail Picture,
    IEnumerable<string>? Tags,

    // New
    string? EmbedHtml,

    string? DefaultLanguage,
    NameAndDescription? LocalizationInfo,

    string PrivacyStatus,

    int VideoCount) : GenericImportable(
        ProviderId: ProviderId, ProviderItemId: ProviderItemId, ETag: ETag, ChannelId: ChannelId, ChannelName: ChannelName, 
        Name: Name, Description: Description, PublishedAt: PublishedAt, Thumbnail: Thumbnail, Picture: Picture, Tags: Tags);

public static class GenericPlaylistExtensions
{
    public static EntityOrigin ToEntityOrigin(this GenericPlaylist gpl)
        => new(ProviderId: gpl.ProviderId,
                ProviderItemId: gpl.ProviderItemId,
                ETag: gpl.ETag,
                ChannelId: gpl.ChannelId,
                ChannelName: gpl.ChannelName,
                Name: gpl.Name,
                Description: gpl.Description,
                PublishedOn: gpl.PublishedAt,
                Thumbnail: gpl.Thumbnail,
                Picture: gpl.Picture,
                EmbedHtml: gpl.EmbedHtml,
                DefaultLanguage: gpl.DefaultLanguage);


    public static Playlist ToPlaylist(this GenericPlaylist gpl)
    {
        var res = new Playlist(gpl.Name, gpl.Description);
        res.SetOrigin(gpl.ToEntityOrigin());
        return res;
    }
}