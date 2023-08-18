using Domain.Videos;
using Infrastructure.Data.Configurations.Entities;

namespace Infrastructure.SqlServer.Configurations;

public class SqlServerVideoConfiguration : VideoConfiguration
{
    public const string TableName = "Videos";

    public const string TagsSequenceName = "TagsSequence";
    public const string SequenceName = "VideoSequence";

    public override void Configure(EntityTypeBuilder<Video> builder)
    {
        base.Configure(builder);

        builder.ToTable(TableName, Constants.VideomaticDbSchema);

        builder.Property(x => x.Id)
               .HasDefaultValueSql($"NEXT VALUE FOR {SequenceName}")
               .IsRequired();
        
    }
}