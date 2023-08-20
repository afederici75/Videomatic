using Domain;
using SharedKernel.Model;

namespace Domain.Tests;

/// <summary>
/// Tests that ensure domain objects are well designed.
/// </summary>
public class VideoTests
{
    const string DummyLocation = "youtube.com/v?VCompleteA";


    [Fact]
    public void VideoGetsDefault5ThumbnailsAndNoOther()
    {
        var video = new Video(name: nameof(CreateVideo));
        
        video.Should().NotBeNull();
        video.Tags.Should().BeEmpty(); // No tags yet
    }

    [Fact]
    public void CreateVideo()
    {
        var pubAt = DateTime.UtcNow;

        var video = new Video(name: nameof(CreateVideo));
        video.SetOrigin(new(
            providerId: "YOUTUBE",
            providerItemId: "ABC123",
            etag: "etag",
            channelId: "#videoOwnerChannelId",
            channelName: "#videoOwnerChannelName",
            name: nameof(CreateVideo),
            description: "A complete description",
            publishedOn: pubAt,
            picture: ImageReference.Empty,
            thumbnail: ImageReference.Empty,
            embedHtml: null,
            defaultLanguage: null));


        video.SetDescription("A complete description");

        video.Should().NotBeNull();
        video.Id.Should().Be((VideoId)0);
        video.Name.Should().Be(nameof(CreateVideo));
        video.Description.Should().Be("A complete description");

        video.Tags.Should().BeEmpty(); // No tags yet
        
        video.Origin.Should().NotBeNull();
        video.Origin.ProviderId.Should().Be("YOUTUBE");
        video.Origin.PublishedOn.Should().Be(pubAt);
        video.Origin.ChannelName.Should().Be("#videoOwnerChannelName");
        video.Origin.ChannelId.Should().Be("#videoOwnerChannelId");
    }

    [Fact]
    public void NoDuplicateTagsAndClearTags()
    {
        var video = new Video(
            name: nameof(NoDuplicateTagsAndClearTags));

        // Ensures duplicate tags are not added
        video.Tags.Should().BeEmpty();

        video.SetTags(new[] { "TAG1", "TAG2", "Tag1" });
        video.Tags.Count().Should().Be(2);

        video.SetTags(new[] { "TAG3" });
        video.Tags.Count().Should().Be(1);

        // Clear tags and verifies
        video.ClearTags();
        video.Tags.Count().Should().Be(0);
    }

    //[Fact]
    //public void SetThumbnails()
    //{
    //    var video = Video.Create(location: DummyLocation, name: nameof(SetThumbnails),
    //        picture: new Thumbnail("http://1", -1, -1),
    //        thumbnail: new Thumbnail("http://2", -1, -1));

    //    var oldDefault = video.GetThumbnail(ThumbnailResolution.Default);
    //    var oldMedium = video.GetThumbnail(ThumbnailResolution.Medium); 

    //    video.SetThumbnail(resolution: ThumbnailResolution.Default, location: "newlocation", height: 100, width: 100);
    //    var newDefault = video.GetThumbnail(ThumbnailResolution.Default);   

    //    video.Thumbnails.Count.Should().Be(5); // Still 5 thumbnails

    //    newDefault.Resolution.Should().Be(oldDefault.Resolution); 
    //    newDefault.Location.Should().NotBe(oldDefault.Location);
    //    newDefault.Height.Should().NotBe(oldDefault.Height);
    //    newDefault.Width.Should().NotBe(oldDefault.Width);
    //}
    
}
