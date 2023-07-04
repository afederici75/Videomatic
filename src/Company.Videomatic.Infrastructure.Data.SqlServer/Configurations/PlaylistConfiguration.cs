namespace Company.Videomatic.Infrastructure.Data.SqlServer.Configurations;

public class PlaylistConfiguration : PlaylistConfigurationBase
{
    public const string SequenceName = "PlaylistSequence";

    public override void Configure(EntityTypeBuilder<Playlist> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Id)            
               .HasDefaultValueSql($"NEXT VALUE FOR {SequenceName}"); // TODO: unhardcode
    }
}
