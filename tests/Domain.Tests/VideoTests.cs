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
        var video = Video.Create(
            location: DummyLocation,
            name: nameof(CreateVideo));
        
        video.Should().NotBeNull();
        video.Thumbnails.Should().HaveCount(5); // It got 5 default thumbnail resolutions
        video.Tags.Should().BeEmpty(); // No tags yet
        //video.Playlists.Should().BeEmpty(); // No playlists yet 
    }

    [Fact]
    public void CreateVideo()
    {
        var pubAt = DateTime.UtcNow;

        var video = Video.Create(
            location: DummyLocation, 
            name: nameof(CreateVideo),
            details: new("YOUTUBE", "ABC123", pubAt, "#videoOwnerChannelTitle", "#videoOwnerChannelId"),
            description: "A complete description");

        video.Should().NotBeNull();
        video.Id.Should().BeNull();
        video.Location.Should().Be(DummyLocation);
        video.Name.Should().Be(nameof(CreateVideo));
        video.Description.Should().Be("A complete description");

        //video.Playlists.Should().BeEmpty(); // No playlists yet 
        video.Tags.Should().BeEmpty(); // No tags yet
        video.Thumbnails.Should().HaveCount(5); // It got 5 default thumbnail resolutions
        
        video.Details.Should().NotBeNull();
        video.Details.Provider.Should().Be("YOUTUBE");
        video.Details.VideoPublishedAt.Should().Be(pubAt);
        //video.Details.ChannelId.Should().Be("#channelId");
        //video.Details.PlaylistId.Should().Be("#playlist");
        //video.Details.Position.Should().Be(1);
        video.Details.VideoOwnerChannelTitle.Should().Be("#videoOwnerChannelTitle");
        video.Details.VideoOwnerChannelId.Should().Be("#videoOwnerChannelId");
    }

    [Fact]
    public void NoDuplicateTagsAndClearTags()
    {
        var video = Video.Create(location: DummyLocation, name: nameof(SetThumbnails));

        // Ensures duplicate tags are not added
        video.Tags.Should().BeEmpty();

        video.AddTags("TAG1", "TAG2");
        video.Tags.Count.Should().Be(2);

        video.AddTags("TAG1");
        video.Tags.Count.Should().Be(2);

        // Clear tags and verifies
        video.ClearTags();
        video.Tags.Count.Should().Be(0);
    }

    [Fact]
    public void SetThumbnails()
    {
        var video = Video.Create(location: DummyLocation, name: nameof(SetThumbnails));        
        var oldDefault = video.GetThumbnail(ThumbnailResolution.Default);
        var oldMedium = video.GetThumbnail(ThumbnailResolution.Medium); 

        video.SetThumbnail(resolution: ThumbnailResolution.Default, location: "newlocation", height: 100, width: 100);
        var newDefault = video.GetThumbnail(ThumbnailResolution.Default);   

        video.Thumbnails.Count.Should().Be(5); // Still 5 thumbnails

        newDefault.Resolution.Should().Be(oldDefault.Resolution); 
        newDefault.Location.Should().NotBe(oldDefault.Location);
        newDefault.Height.Should().NotBe(oldDefault.Height);
        newDefault.Width.Should().NotBe(oldDefault.Width);
    }
    
}
