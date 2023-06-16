namespace Company.Videomatic.Domain.Tests;

public class CRUDTests
{
    [Fact]
    public void AddCollection()
    {
        Collection collection = new (name: "", description: "A collection of videos about philosophy.");
        
        Video video1 = new("https://youtube?v=HINDU", "Hinduism explained", "A short introduction to Hinduism.");
        
        Thumbnail thumbv1_1 = new("http://youtube/thumbs/v1_1", ThumbnailResolution.Standard, 200, 200);
        Thumbnail thumbv1_2 = new("http://youtube/thumbs/v1_2", ThumbnailResolution.Medium, 300, 300);
        Thumbnail thumbv1_3 = new("http://youtube/thumbs/v1_3", ThumbnailResolution.High, 400, 400);

        Video video2 = new("https://youtube?v=TAO", "Taoism explained", "A short introduction to Taoism.");

        collection.AddVideo(video1)
                  .AddVideo(video2);
    }
}
