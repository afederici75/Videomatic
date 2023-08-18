using Infrastructure.Data.Configurations.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Infrastructure.Data.SqlServer.Configurations;

public class SqlServerPlaylistConfiguration : PlaylistConfiguration
{
    public const string TableName = "Playlists";
    public const string SequenceName = "PlaylistSequence";

    public override void Configure(EntityTypeBuilder<Playlist> builder)
    {
        base.Configure(builder);

        builder.ToTable(TableName, Constants.VideomaticDbSchema);

        builder.Property(x => x.Id)            
               .HasDefaultValueSql($"NEXT VALUE FOR {SequenceName}")
               .IsRequired(); // TODO: unhardcode        
    }
}
