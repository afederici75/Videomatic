using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Videomatic.Infrastructure.SqlServer;

public static class DbConstants
{
    public const string SequenceName = "IdSequence";

    public static class FieldLengths
    {     
        public const int Url = 1024;
        public const int ProviderId = 20;
        public const int FolderName = 120;
        public const int YTVideoTitle = 100;
        public const int YTVideoDescription = 5000; // See https://developers.google.com/youtube/v3/docs/videos snippet.description
        public const int YTTranscriptLineText = 100;
        public const int ArtifactTitle = 450;
        //public const int ArtifactSummary = 450;
    }
}
