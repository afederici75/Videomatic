using Infrastructure.Data.Configurations.Entities;

namespace Infrastructure.Data.SqlServer.Configurations;

public class SqlServerPlaylistConfiguration : PlaylistConfiguration
{
    public const string SequenceName = "PlaylistSequence";

    public override void Configure(EntityTypeBuilder<Playlist> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Id)            
               .HasDefaultValueSql($"NEXT VALUE FOR {SequenceName}"); // TODO: unhardcode        
    }
}
