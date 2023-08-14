using AutoMapper.Internal;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.SqlServer;

public static class FullTextIndexingMigrationHelper
{
    static string[] GetStringPropertiesOf<T>()
    { 
        return typeof(T)
            .GetProperties()                        
            .Where(p => p.PropertyType == typeof(string) && p.IsPublic())                        
            .Select(p => p.Name)                        
            .ToArray();
    }

    static string GetFullTextFieldsOf<T>()
    {
        return string.Join(", ", GetStringPropertiesOf<T>());
    }

    public static void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(
            sql: "CREATE FULLTEXT CATALOG FTVideomatic AS DEFAULT;",
            suppressTransaction: true);



        migrationBuilder.Sql(
            sql: $@"CREATE FULLTEXT INDEX ON {VideomaticConstants.VideomaticSchema}.{nameof(Video)}s (
                                {nameof(Video.Name)},
                                {nameof(Video.Description)}, 
                                Origin_ProviderId,
                                Origin_ProviderItemId,
                                Origin_ETag,
                                Origin_ChannelId,
                                Origin_ChannelName)
                       KEY INDEX PK_Videos ON FTVideomatic
                       WITH STOPLIST = OFF, CHANGE_TRACKING AUTO;",
            suppressTransaction: true);

        migrationBuilder.Sql(
            sql: $@"CREATE FULLTEXT INDEX ON {VideomaticConstants.VideomaticSchema}.{nameof(Playlist)}s  (
                                {nameof(Playlist.Name)},
                                {nameof(Playlist.Description)},
                                Origin_ProviderId,
                                Origin_ProviderItemId,
                                Origin_ETag,
                                Origin_ChannelId,
                                Origin_ChannelName)
                       KEY INDEX PK_Playlists ON FTVideomatic
                       WITH STOPLIST = OFF, CHANGE_TRACKING AUTO;",
            suppressTransaction: true);

        migrationBuilder.Sql(
            sql: $@"CREATE FULLTEXT INDEX ON {VideomaticConstants.VideomaticSchema}.{nameof(Artifact)}s (
                                {nameof(Artifact.Name)}, 
                                {nameof(Artifact.Type)}, 
                                {nameof(Artifact.Text)})
                       KEY INDEX PK_Artifacts ON FTVideomatic
                       WITH STOPLIST = OFF, CHANGE_TRACKING AUTO;",
            suppressTransaction: true);

        migrationBuilder.Sql(
            sql: $@"CREATE FULLTEXT INDEX ON {VideomaticConstants.VideomaticSchema}.{nameof(Transcript)}s (
                                {nameof(Transcript.Language)})
                       KEY INDEX PK_Transcripts ON FTVideomatic
                       WITH STOPLIST = OFF, CHANGE_TRACKING AUTO;",
            suppressTransaction: true);

        migrationBuilder.Sql(
            sql: $@"CREATE FULLTEXT INDEX ON {VideomaticConstants.VideomaticSchema}.{nameof(TranscriptLine)}s (
                                {nameof(TranscriptLine.Text)})
                       KEY INDEX PK_TranscriptLines ON FTVideomatic
                       WITH STOPLIST = OFF, CHANGE_TRACKING AUTO;",
            suppressTransaction: true);
    }
}
