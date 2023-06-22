using FluentAssertions;
using FluentValidation;
using Xunit;
using Xunit.DependencyInjection;

namespace Company.Videomatic.Application.Tests;

/// <summary>
/// Tests that verify FluentValidation is working as expected.
/// Not all validators are tested here, just a few to verify the setup is working.
/// </summary>
public class ValidationTests
{
    public ValidationTests(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    IServiceProvider ServiceProvider { get; }

    [Fact]
    public void ValidatesFilter()
    {        
        var v = new FilterValidator<Filter>();

        // No filters specified (INVALID)
        var filter = new Filter();
        var res = v.Validate(filter);
        res.Errors.Should().HaveCount(1); 
        
        // Individual properties (INVALID)
        filter = new Filter(
            SearchText: new string('x', FilterValidator<Filter>.Lengths.MaxSearchTextLength + 1), // Too long
            Ids: Array.Empty<long>(), // Empty
            Items: Array.Empty<FilterItem>()); // Empty
        res = v.Validate(filter);
        res.Errors.Should().HaveCount(3);

        // Items.PropertyName
        filter = new Filter(SearchText: null, Ids: null, Items: new[] { new FilterItem("") });
        res = v.Validate(filter);
        res.Errors.Should().HaveCount(1); // PropertyName should be 1-128 chars


        // All specified (VALID)
        filter = new Filter(SearchText: "12345", Ids: new long[] { 1, 2 }, Items: new[] { new FilterItem("x") });
        res = v.Validate(filter);
        res.Errors.Should().BeEmpty();

        // One specified (VALID)
        filter = new Filter(SearchText: "ABCDE", Ids: null, Items: null);
        res = v.Validate(filter);
        res.Errors.Should().BeEmpty();

        filter = new Filter(SearchText: null, Ids: new long[] { 1 }, Items: null);
        res = v.Validate(filter);
        res.Errors.Should().BeEmpty();

        filter = new Filter(SearchText: null, Ids: null, Items: new[] { new FilterItem("y") });
        res = v.Validate(filter);
        res.Errors.Should().BeEmpty();
    }
}