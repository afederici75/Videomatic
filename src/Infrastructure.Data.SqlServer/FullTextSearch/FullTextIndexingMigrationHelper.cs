using AutoMapper.Internal;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.SqlServer.FullTextSearch;

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
            sql: $@"CREATE FULLTEXT INDEX ON {Constants.VideomaticDbSchema}.{nameof(Video)}s (
                                {nameof(Video.Name)},
                                {nameof(Video.Description)}, 
                                {nameof(Video.Tags)},
                                {nameof(Video.CreatedBy)},
                                {nameof(Video.UpdatedBy)},
                                Origin_ProviderId,
                                Origin_ProviderItemId,
                                Origin_ETag,
                                Origin_ChannelId,
                                Origin_ChannelName)
                       KEY INDEX PK_Videos ON FTVideomatic
                       WITH STOPLIST = OFF, CHANGE_TRACKING AUTO;",
            suppressTransaction: true);

        migrationBuilder.Sql(
            sql: $@"CREATE FULLTEXT INDEX ON {Constants.VideomaticDbSchema}.{nameof(Playlist)}s  (
                                {nameof(Playlist.Name)},
                                {nameof(Playlist.Description)},
                                {nameof(Playlist.Tags)},
                                {nameof(Video.CreatedBy)},
                                {nameof(Video.UpdatedBy)},                                
                                Origin_ProviderId,
                                Origin_ProviderItemId,
                                Origin_ETag,
                                Origin_ChannelId,
                                Origin_ChannelName)
                       KEY INDEX PK_Playlists ON FTVideomatic
                       WITH STOPLIST = OFF, CHANGE_TRACKING AUTO;",
            suppressTransaction: true);

        migrationBuilder.Sql(
            sql: $@"CREATE FULLTEXT INDEX ON {Constants.VideomaticDbSchema}.{nameof(Artifact)}s (
                                {nameof(Artifact.Name)}, 
                                {nameof(Artifact.Type)}, 
                                {nameof(Artifact.Text)},
                                {nameof(Video.CreatedBy)},
                                {nameof(Video.UpdatedBy)})
                       KEY INDEX PK_Artifacts ON FTVideomatic
                       WITH STOPLIST = OFF, CHANGE_TRACKING AUTO;",
            suppressTransaction: true);

        migrationBuilder.Sql(
            sql: $@"CREATE FULLTEXT INDEX ON {Constants.VideomaticDbSchema}.{nameof(Transcript)}s (
                                {nameof(Transcript.Language)},
                                {nameof(Transcript.Lines)},
                                {nameof(Video.CreatedBy)},
                                {nameof(Video.UpdatedBy)})
                       KEY INDEX PK_Transcripts ON FTVideomatic
                       WITH STOPLIST = OFF, CHANGE_TRACKING AUTO;",
            suppressTransaction: true);


    }
}
