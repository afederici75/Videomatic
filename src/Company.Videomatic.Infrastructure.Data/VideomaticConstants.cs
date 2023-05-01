namespace Company.Videomatic.Infrastructure.Data;

public static class VideomaticConstants
{
    public const string MigrationAssemblyNamePrefix = "Company.Videomatic.Infrastructure.Data.";
    public const string Videomatic = "Videomatic";

    public const string SequenceName = "IdSequence";

    public static class DbFieldLengths
    {     
        public const int Url = 1024;
        public const int ProviderId = 20;
        public const int FolderName = 120;
        public const int YTVideoId = 11; // TODO: likely to change when we support other providers
        public const int YTVideoTitle = 100;
        public const int YTVideoDescription = 5000; // See https://developers.google.com/youtube/v3/docs/videos snippet.description
        public const int YTTranscriptLineText = 100;
        public const int ArtifactTitle = 450;
        //public const int ArtifactSummary = 450;
    }
}
