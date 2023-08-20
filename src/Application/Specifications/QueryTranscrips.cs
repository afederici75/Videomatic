namespace Application.Specifications;

public static class QueryTranscripts
{

    public class ByVideoId : Specification<Transcript>
    {
        /// <summary>
        /// Returns the transcripts linked to the specified videos.
        /// </summary>

        public ByVideoId(IEnumerable<VideoId> videoIds)
        {
            Query.Where(t => videoIds.Contains(t.VideoId));
        }

        /// <summary>
        /// Returns the transcripts linked to the specified video.
        /// </summary>
        /// <param name="videoIds"></param>
        public ByVideoId(VideoId videoId)
        {
            Query.Where(t => videoId == t.VideoId);
        }
    }
}