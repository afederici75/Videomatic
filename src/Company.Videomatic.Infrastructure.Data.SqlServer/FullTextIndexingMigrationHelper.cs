using Company.Videomatic.Domain.Aggregates.Artifact;
using Company.Videomatic.Domain.Aggregates.Transcript;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Videomatic.Infrastructure.Data.SqlServer;

public static class FullTextIndexingMigrationHelper
{
    public static void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(
            sql: "CREATE FULLTEXT CATALOG FTVideomatic AS DEFAULT;",
            suppressTransaction: true);



        migrationBuilder.Sql(
            sql: $@"CREATE FULLTEXT INDEX ON {nameof(Video)}s (
                                {nameof(Video.Name)},
                                {nameof(Video.Description)}, 
                                Details_Provider, 
                                Details_ProviderVideoId, 
                                Details_VideoOwnerChannelTitle, 
                                Details_VideoOwnerChannelId)
                       KEY INDEX PK_Videos ON FTVideomatic
                       WITH STOPLIST = OFF, CHANGE_TRACKING AUTO;",
            suppressTransaction: true);

        migrationBuilder.Sql(
            sql: $@"CREATE FULLTEXT INDEX ON {nameof(Playlist)}s  (
                                {nameof(Playlist.Name)},
                                {nameof(Playlist.Description)})
                       KEY INDEX PK_Playlists ON FTVideomatic
                       WITH STOPLIST = OFF, CHANGE_TRACKING AUTO;",
            suppressTransaction: true);

        migrationBuilder.Sql(
            sql: $@"CREATE FULLTEXT INDEX ON {nameof(Artifact)}s (
                                {nameof(Artifact.Name)}, 
                                {nameof(Artifact.Type)}, 
                                {nameof(Artifact.Text)})
                       KEY INDEX PK_Artifacts ON FTVideomatic
                       WITH STOPLIST = OFF, CHANGE_TRACKING AUTO;",
            suppressTransaction: true);

        migrationBuilder.Sql(
            sql: $@"CREATE FULLTEXT INDEX ON {nameof(Transcript)}s (
                                {nameof(Transcript.Language)})
                       KEY INDEX PK_Transcripts ON FTVideomatic
                       WITH STOPLIST = OFF, CHANGE_TRACKING AUTO;",
            suppressTransaction: true);

        migrationBuilder.Sql(
            sql: $@"CREATE FULLTEXT INDEX ON {nameof(TranscriptLine)}s (
                                {nameof(TranscriptLine.Text)})
                       KEY INDEX PK_TranscriptLines ON FTVideomatic
                       WITH STOPLIST = OFF, CHANGE_TRACKING AUTO;",
            suppressTransaction: true);
    }
}
