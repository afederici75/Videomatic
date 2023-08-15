namespace Infrastructure.Data.Configurations.Helpers;

public class TrackableConfigurator<T> 
    where T : class, ITrackable
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(x => x.CreatedOn);
        //.ValueGeneratedOnAdd()
        //.HasDefaultValueSql("GETUTCDATE()")
        //.Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        builder.Property(x => x.UpdatedOn);
    }

}
