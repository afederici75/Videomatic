using Company.Videomatic.Domain.Video;

namespace Company.Videomatic.Infrastructure.SqlServer.Configurations;

public class VideoConfiguration : VideoConfigurationBase
{
    //public const string ThumbnailSequenceName = "ThumbnailSequence";
    public const string TagsSequenceName = "TagsSequence";
    public const string SequenceName = "VideoSequence";

    public override void Configure(EntityTypeBuilder<Video> builder)
    {
        base.Configure(builder);
        
        builder.Property(x => x.Id)
               .HasDefaultValueSql($"NEXT VALUE FOR {SequenceName}"); // TODO: unhardcode
        
    }
}