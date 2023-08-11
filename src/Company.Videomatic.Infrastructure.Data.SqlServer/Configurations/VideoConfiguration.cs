namespace Company.Videomatic.Infrastructure.SqlServer.Configurations;

public class VideoConfiguration : VideoConfigurationBase
{
    //public const string ThumbnailSequenceName = "ThumbnailSequence";
    public const string TagsSequenceName = "TagsSequence";
    public const string SequenceName = "VideoSequence";

    public override void Configure(EntityTypeBuilder<Video> builder)
    {
        base.Configure(builder);

        //builder.AddSequenceForId();

        builder.Property(x => x.Id)
               .HasDefaultValueSql($"NEXT VALUE FOR {SequenceName}"); // TODO: unhardcode

        //var thumbnails = builder.OwnsMany(x => x.Thumbnails,
        //    (builder) =>
        //    {
        //        builder.Property("Id")
        //               .HasDefaultValueSql($"NEXT VALUE FOR {ThumbnailSequenceName}"); // TODO: unhardcode                
        //    });

        var agss = builder.OwnsMany(x => x.Tags,
            (builder) =>
            {
                builder.Property("Id")
                       .HasDefaultValueSql($"NEXT VALUE FOR {TagsSequenceName}"); // TODO: unhardcode                
            });
    }
}