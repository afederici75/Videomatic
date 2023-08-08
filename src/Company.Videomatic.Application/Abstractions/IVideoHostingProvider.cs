using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Company.Videomatic.Application.Abstractions;

/// <summary>
/// This interface provides access to the YouTube APIs we are interested in.
/// </summary>
public interface IVideoHostingProvider
{
    IAsyncEnumerable<GenericChannel> GetChannelInformationAsync(IEnumerable<string> idsOrUrls, CancellationToken cancellation = default);

    IAsyncEnumerable<GenericPlaylist> GetPlaylistInformationAsync(IEnumerable<string> idsOrUrls, CancellationToken cancellation = default);

    IAsyncEnumerable<GenericVideo> GetVideoInformationAsync(IEnumerable<string> idsOrUrls, CancellationToken cancellation = default);
}

public static class IVideoHostingProviderExtensions
{
    public static async Task<Result<GenericPlaylist>> GetPlaylistInformationAsync(this IVideoHostingProvider instance, string idsOrUrl, CancellationToken cancellation = default)
    {
        await foreach (var item in instance.GetPlaylistInformationAsync(new[] { idsOrUrl }, cancellation))
        {
            return Result.Success(item);
        }

        return Result.NotFound();
    }

    public static async Task<Result<GenericChannel>> GetChannelInformationAsync(this IVideoHostingProvider instance, string idsOrUrl, CancellationToken cancellation = default)
    {
        await foreach (var item in instance.GetChannelInformationAsync(new[] { idsOrUrl }, cancellation))
        {
            return Result.Success(item);
        }

        return Result.NotFound();
    }

    public static async Task<Result<GenericVideo>> GetVideoInformationAsync(this IVideoHostingProvider instance, string idsOrUrl, CancellationToken cancellation = default)
    {
        await foreach (var item in instance.GetVideoInformationAsync(new[] { idsOrUrl }, cancellation))
        {
            return Result.Success(item);
        }

        return Result.NotFound();
    }
}

public record GenericChannel(
    string Id,
    string ETag,

    string Name,
    string? Description,

    DateTime PublishedAt,

    string? ThumbnailUrl,
    string? PictureUrl,

    string? DefaultLanguage,
    LocalizationInfo? LocalizationInfo,

    ContentOwnerDetail? ContentOwnerDetail,
    GenericChannelTopics? Topics,
    GenericChannelStatistcs? Statistics);


public record GenericChannelTopics(
    IEnumerable<string> TopicIds,
    IEnumerable<string> TopicCategories);

public record LocalizationInfo(
    string Name, 
    string? Description);

public record ContentOwnerDetail(
    string ContentOwner, 
    DateTime? TimeLinked);

public record GenericChannelStatistcs(
    ulong? VideoCount,
    ulong? SuscriberCount,
    ulong? ViewCount);

public record GenericPlaylist(
    string Id,
    string ETag,
    string ChannelId,

    string Name,
    string? Description,

    DateTime PublishedAt,
    
    string? ThumbnailUrl,
    string? PictureUrl,
    string? EmbedHtml, 

    string? DefaultLanguage,
    LocalizationInfo LocalizationInfo,

    string PrivacyStatus,

    int VideoCount);

public record GenericVideo(
    string Id,
    string ETag,
    string ChannelId,

    string Name,
    string? Description,

    DateTime PublishedAt,

    string? ThumbnailUrl,
    string? PictureUrl,
    string? EmbedHtml,

    string? DefaultLanguage,
    LocalizationInfo LocalizationInfo,
    string PrivacyStatus);