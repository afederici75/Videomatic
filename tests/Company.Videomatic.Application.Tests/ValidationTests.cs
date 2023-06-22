using Company.Videomatic.Application.Features.Model;
using Company.Videomatic.Application.Query;
using FluentAssertions;
using Xunit;

namespace Company.Videomatic.Application.Tests;

public class ValidationTests
{
    [Fact]
    public void ValidatesFilter()
    {
        var v = new FilterValidator<Filter>();

        // No filters specified (INVALID)
        var filter = new Filter();
        var res = v.Validate(filter);
        res.Errors.Should().HaveCount(1); // SearchText, Ids or Items must be specified
        
        // Individual properties (INVALID)
        filter = new Filter(
            SearchText: new string('x', 30000), 
            Ids: Array.Empty<long>(), 
            Items: Array.Empty<FilterItem>());
        res = v.Validate(filter);
        res.Errors.Should().HaveCount(3);

        // All specified (VALID)
        filter = new Filter(SearchText: "12345", Ids: new long[] { 1, 2 }, Items: new[] { new FilterItem("xxx") });
        res = v.Validate(filter);
        res.Errors.Should().BeEmpty();

        // One specified (VALID)
        filter = new Filter(SearchText: "ABCDE", Ids: null, Items: null);
        res = v.Validate(filter);
        res.Errors.Should().BeEmpty();

        filter = new Filter(SearchText: null, Ids: new long[] { 1 }, Items: null);
        res = v.Validate(filter);
        res.Errors.Should().BeEmpty();

        filter = new Filter(SearchText: null, Ids: null, Items: new[] { new FilterItem("xxx") });
        res = v.Validate(filter);
        res.Errors.Should().BeEmpty();
    }
}