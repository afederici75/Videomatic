using Infrastructure.Tests.Data.Helpers;

namespace Infrastructure.Tests.Data;

[Collection("DbContextTests")]
public class FullTextSearchTests : IClassFixture<DbContextFixture>//, IAsyncLifetime
{
    public FullTextSearchTests(
        DbContextFixture fixture,
        ITestOutputHelperAccessor outputAccessor,
        ISender sender)
    {
        Output = outputAccessor?.Output ?? throw new ArgumentNullException(nameof(outputAccessor));
        Fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
        Sender = sender ?? throw new ArgumentNullException(nameof(sender));      
    }
  
    public ITestOutputHelper Output { get; }
    public DbContextFixture Fixture { get; }
    public ISender Sender { get; }

    [Fact]
    public async Task QueryStuff()
    {
        var db = Fixture.DbContext;
        var res = await db.Videos.Where(v => v.Id == 1).FirstOrDefaultAsync();

        res.Should().NotBeNull();
    }

    [Theory]
    [InlineData("gods", 1)]
    [InlineData("god", 1)]
    [InlineData("huxley", 1)]
    [InlineData("huxley gods", 2)]
    [InlineData("huxley AND gods", 2)] // It's searching on all 3 terms!
    public async Task FT_VideoFreeText(string freeText, int expectedResults)
    {
        var db = Fixture.DbContext;

        // FreeText is more restrictive predicate which by default does a search based
        // on various forms of a word or phrase (that means it by default includes Inflectional
        // forms as well as thesaurus).

        var cnt = await db.Videos.CountAsync();
        cnt.Should().Be(2);

        var res = await db.Videos.Where(x => 
            EF.Functions.FreeText(x.Name, $"{freeText}") ||
            EF.Functions.FreeText(x.Description!, $"{freeText}"))
            .ToListAsync();

        var allVids = await db.Videos.ToListAsync();
        res.Count.Should().Be(expectedResults);
    }

    [Theory]
    [InlineData("gods", 1)]
    [InlineData("god", 0)] // Does not find plural like 'gods'
    [InlineData("FORMSOF(Inflectional, god)", 1)]
    [InlineData("FORMSOF(Inflectional, gods)", 1)]
    [InlineData("huxley", 1)]
    [InlineData("huxl", 0)]
    [InlineData("\"hux*\"", 1)]
    [InlineData("huxj*", 0)]
    [InlineData("\"huxley OR gods\"", 0)] // BAD - Needs ""
    [InlineData("\"huxley\" OR \"gods\"", 2)]
    [InlineData("\"huxley\" AND \"god\"", 0)]
    //[InlineData("huxley gods", 0)] // // Throws exception because it does not have ""
    [InlineData("\"huxley AND gods\"", 0)] // 
    [InlineData("\"huxley\" AND \"gods\"", 0)]
    [InlineData("\"describes\" OR \"offer\"", 2)] 
    public async Task FT_VideoContains(string contains, int expectedResults)
    {
        var db = Fixture.DbContext;

        // Contains, unlike FreeText, gives you flexibility to do various forms of search separately.
        var res = await db.Videos.Where(x =>
            EF.Functions.Contains(x.Name, $"{contains}") ||
            EF.Functions.Contains(x.Description!, $"{contains}"))
            .ToListAsync();

        try
        {
            res.Count.Should().Be(expectedResults);
        }
        catch 
        {
            res = await db.Videos.Where(x =>
                EF.Functions.Contains(x.Name, $"{contains}") ||
                EF.Functions.Contains(x.Description!, $"{contains}"))
                .ToListAsync();

            res.Count.Should().Be(expectedResults);
        }
    }

}