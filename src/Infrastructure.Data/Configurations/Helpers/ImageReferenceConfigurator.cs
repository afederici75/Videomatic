namespace Infrastructure.Data.Configurations.Helpers;

public static class ImageReferenceConfigurator
{
    public static void Configure<T>(OwnedNavigationBuilder<T, ImageReference> bld)
        where T : class
    {
        bld.Property(x => x.Url).HasMaxLength(FieldLengths.Generic.Url);
        bld.Property(x => x.Width);
        bld.Property(x => x.Height);
    }
}
