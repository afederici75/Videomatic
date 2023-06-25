using Company.Videomatic.Application.Features.Playlists.Queries;
using FluentValidation.TestHelper;
using Xunit;

namespace Company.Videomatic.Application.Tests;

/// <summary>
/// Tests that verify FluentValidation is working as expected.
/// Not all validators are tested here, just a few to verify the setup is working.
/// See https://docs.fluentvalidation.net/en/latest/testing.html
/// </summary>
public class ValidationTests
{    
    [Fact]
    public void AcceptFilter()
    {
        //var v = new FilterValidator();
        //
        //var filter = new Filter(SearchText: "ABCDE", Ids: null, Items: null);
        //var res = v.TestValidate(filter);
        //res.ShouldNotHaveAnyValidationErrors();
        //
        //filter = new Filter(SearchText: null, Ids: new long[] { 1 }, Items: null);
        //res = v.TestValidate(filter);
        //res.ShouldNotHaveAnyValidationErrors();
        //
        //filter = new Filter(SearchText: null, Ids: null, Items: new[] { new FilterItem("y") });
        //res = v.TestValidate(filter);
        //res.ShouldNotHaveAnyValidationErrors();
        throw new Exception();
    }

    [Fact]
    public void RejectFilter()
    {
        //var v = new FilterValidator();

        //// No filters specified 
        //var filter = new Filter();
        //var res = v.TestValidate(filter);
        //res.ShouldHaveValidationErrorFor(""); // The object itself: invalid state with no property set

        //// Individual properties (INVALID)
        //filter = new Filter(
        //    SearchText: new string('x', FilterValidatorBase<Filter>.Lengths.MaxSearchTextLength + 1), // Too long
        //    Ids: Array.Empty<long>(), // Empty
        //    Items: Array.Empty<FilterItem>()); // Empty
        //res = v.TestValidate(filter);
        //res.Errors.Should().HaveCount(3);
        //res.ShouldHaveValidationErrorFor(x => x.SearchText);
        //res.ShouldHaveValidationErrorFor(x => x.Ids);
        //res.ShouldHaveValidationErrorFor(x => x.Items);

        //// Items.PropertyName invalid (ensures FilterItemValidator is used)
        //filter = new Filter(SearchText: null, Ids: null, Items: new[] { new FilterItem("") }); // Empty string
        //res = v.TestValidate(filter);
        //res.ShouldHaveValidationErrorFor("Items[0].Property"); // PropertyName should be 1-128 chars        

        throw new Exception();
    }

    [Fact]
    public void AcceptGetPlaylistsQuery()
    {
        var v = new GetPlaylistsQueryValidator();

        // No filters specified (VALID)
        var qry = new GetPlaylistsQuery(); 
        var res = v.TestValidate(qry);
        res.ShouldNotHaveAnyValidationErrors();       
    }

    [Fact]
    public void RejectGetPlaylistsQuery()
    {
        var v = new GetPlaylistsQueryValidator();
        throw new Exception();

        // Individual properties (INVALID)
        //var qry = new GetPlaylistsQuery(OrderBy: new OrderBy(new OrderByItem[] { }));
        //var res = v.TestValidate(qry);
        //res.ShouldHaveValidationErrorFor(x => x.OrderBy!.Items); // Should not be empty
        //res.Errors.Count.Should().Be(1);    
        //
        //// Individual properties (INVALID)
        //qry = new GetPlaylistsQuery(true, -1, -2);
        //res = v.TestValidate(qry);
        //res.ShouldHaveValidationErrorFor(x => x.Filter!.Ids); // Negative numbers
        //res.Errors.Count.Should().Be(2);
        //
        //// Individual properties (INVALID)
        //qry = new GetPlaylistsQuery(false, Array.Empty<long>());
        //res = v.TestValidate(qry);
        //res.ShouldHaveValidationErrorFor(x => x.Filter!.Ids); // Empty
        //res.Errors.Count.Should().Be(1);
    }
}