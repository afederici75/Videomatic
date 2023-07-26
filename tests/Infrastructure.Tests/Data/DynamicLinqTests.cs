using Azure.Core;
using Company.Videomatic.Application.Features.Videos;
using Company.Videomatic.Domain.Aggregates.Video;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel.Orchestration;
using System.Linq.Dynamic.Core;

namespace Infrastructure.Tests.Data;

[Collection("DbContextTests")]
public class DynamicLinqTests
{
    public DynamicLinqTests(
        IDbContextFactory<VideomaticDbContext> dbContextFactory,
        ITestOutputHelperAccessor outputAccessor,
        ISender sender)
    {
        DbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
        Output = outputAccessor?.Output ?? throw new ArgumentNullException(nameof(outputAccessor));

        Sender = sender ?? throw new ArgumentNullException(nameof(sender));
    }

    public IDbContextFactory<VideomaticDbContext> DbContextFactory { get; }
    public ITestOutputHelper Output { get; }
    public ISender Sender { get; }

    [Theory]
    //[InlineData("", "", null, null)]
    [InlineData(@"(Name.Contains(""a""))", "", null, null)]
    public async Task TestDynamicLinq(string? whereText, string? orderByText, int? take = null, int? skip = null)
    {
        using var db = await DbContextFactory.CreateDbContextAsync();

        // --- Sets up the main query
        IQueryable<Video> q = db.Videos.AsQueryable();

        if (!string.IsNullOrWhiteSpace(whereText))
            q = q.Where(whereText);

        if (!string.IsNullOrWhiteSpace(orderByText))
            q = q.OrderBy(orderByText);

        #region Paging
        if (take.HasValue)
            q = q.Take(take.Value);

        if (skip.HasValue)
            q = q.Skip(skip.Value);
        #endregion

        var final = q.Select(v => new VideoDTO(
        v.Id,
            v.Location,
            v.Name,
            v.Description,
            null, //request.IncludeTags ? v.Tags.Select(t => t.Name) : null,
            null,//includeThumbnail ? v.Thumbnails.Single(t => t.Resolution == preferredRes).Location : null,
            db.Artifacts.Count(a => a.VideoId == v.Id),
            db.Transcripts.Count(a => a.VideoId == v.Id),
            v.Tags.Count(),
            v.Details.Provider,
            v.Details.ProviderVideoId,
            v.Details.VideoPublishedAt,
            v.Details.VideoOwnerChannelTitle,
            v.Details.VideoOwnerChannelId
            ));

        var results = await q.ToListAsync();
        
        var queryText = q.ToQueryString();
        Output.WriteLine($"{results.Count} Result(s).");
        Output.WriteLine($"{queryText}:\n{queryText}");        
    }
}