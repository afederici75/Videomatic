using Infrastructure.Data.Configurations.Entities;

namespace Infrastructure.Data.SqlServer.Configurations;

public class SqlServerArtifactConfiguration : ArtifactConfiguration 
{
    public const string TableName = "Artifacts";
    public const string SequenceName = "ArtifactSequence";

    public override void Configure(EntityTypeBuilder<Artifact> builder)
    {
        base.Configure(builder);

        builder.ToTable(TableName, Constants.VideomaticDbSchema);

        builder.Property(x => x.Id)
               .HasDefaultValueSql($"NEXT VALUE FOR {SequenceName}")
               .IsRequired(); // TODO: unhardcode
    }
}