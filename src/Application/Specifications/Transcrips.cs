namespace Application.Specifications;

public static class Transcripts
{
    public class ByVideoId : Specification<Transcript>
    {
        public ByVideoId(IEnumerable<VideoId> videoIds)
        {
            Query.Where(t => videoIds.Contains(t.VideoId));
        }
    }
}