using Azure.Core;
using Company.Videomatic.Application.Features.Videos;
using Company.Videomatic.Domain.Aggregates.Video;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel.Orchestration;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

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
    [InlineData($"(Name.Contains(\"a\"))", "", null, null)]
    [InlineData($"(TagCount > 0)", "", null, null)]
    [InlineData(null, "TagCount DESC", null, null)]
    [InlineData(null, "ArtifactCount DESC", null, null)]
    public async Task TestDynamicLinq(string? whereText, string? orderByText, int? take = null, int? skip = null)
    {
        using var db = await DbContextFactory.CreateDbContextAsync();

        var aliases = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "TagCount", "Tags.Count()" },
            { "ArtifactCount", "Artifacts.Count()" },
            { "TranscriptCount", "Transcripts.Count()" }
        };
        foreach (var d in aliases)
        {
            whereText = whereText?.Replace(d.Key, d.Value);
            orderByText = orderByText?.Replace(d.Key, d.Value);
        }

        // --- Sets up the main query
        IQueryable<Video> q = db.Videos
            .AsSplitQuery()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(whereText))
            q = q.Where(whereText);

        //if (!string.IsNullOrWhiteSpace(orderByText))
        //    q = q.OrderBy(orderByText);
        //else
        //    q = q.OrderBy("Id");
        //
        //q = q.Take(take ?? 10).Skip(skip ?? 0);

        var final = q.Select(v => new VideoDTO(
        v.Id,
            v.Location,
            v.Name,
            v.Description,
            v.Tags.Select(t => t.Name),// : null,
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

        if (!string.IsNullOrWhiteSpace(orderByText))
            final = final.OrderBy(orderByText);
        else
            final = final.OrderBy("Id");

        final = final.Take(take ?? 10).Skip(skip ?? 0);


        var results = await final.ToListAsync();

        var queryText = final.ToQueryString();
        Output.WriteLine($"{results.Count} Result(s).");
        Output.WriteLine($"------------------------------------");
        Output.WriteLine($"{queryText}");
    }

    [Theory]
    //[InlineData("", "", null, null)]
    //[InlineData($"(Name.Contains(\"a\"))", "", null, null)]
    //[InlineData($"(TagCount > 0)", "", null, null)]
    //[InlineData(null, "TagCount DESC", null, null)]
    [InlineData(null, "ArtifactCount DESC", null, null)]
    public async Task TestDynamicLinq2(string? whereText, string? orderByText, int? take = null, int? skip = null)
    {
        using var db = await DbContextFactory.CreateDbContextAsync();

        var aliases = new Dictionary<string, Expression<Func<VideomaticDbContext, VideoDTO, object?>>>(StringComparer.OrdinalIgnoreCase)
        {
            //{ "ArtifactCount", "Artifacts.Count()" },
            //{ "TranscriptCount", "Transcripts.Count()" }
        };

        //foreach (var d in aliases)
        //{
        //    whereText = whereText?.Replace(d.Key, d.Value);
        //    orderByText = orderByText?.Replace(d.Key, d.Value);
        //}

        // --- Sets up the main query
        IQueryable<Video> q = db.Videos
            .AsSplitQuery()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(whereText))
            q = q.Where(whereText);

        var final = q.Select(v => new VideoDTO(
            v.Id,
            v.Location,
            v.Name,
            v.Description,
            v.Tags.Select(t => t.Name),// : null,
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

        if (!string.IsNullOrWhiteSpace(orderByText))
            final = final.OrderBy(orderByText);
        else
            final = final.OrderBy("Id");

        final = final.Take(take ?? 10).Skip(skip ?? 0);


        var results = await final.ToListAsync();

        var queryText = final.ToQueryString();
        Output.WriteLine($"{results.Count} Result(s).");
        Output.WriteLine($"------------------------------------");
        Output.WriteLine($"{queryText}");
    }

    [Theory]
    //[InlineData("", "", null, null)]
    //[InlineData($"(Name.Contains(\"a\"))", "", null, null)]
    //[InlineData($"(TagCount > 0)", "", null, null)]
    //[InlineData(null, "TagCount DESC", null, null)]
    [InlineData(null, "ArtifactCount DESC", null, null)]
    public async Task TestFullTextContains(string? whereText, string? orderByText, int? take = null, int? skip = null)
    {
        using var db = await DbContextFactory.CreateDbContextAsync();

        var res = await db.Videos.Where(x => EF.Functions.Contains(
            x.Description,
            "(Android or Shiva) or (gaming) or (chloe near craig)"))
            .ToListAsync();
    }
}