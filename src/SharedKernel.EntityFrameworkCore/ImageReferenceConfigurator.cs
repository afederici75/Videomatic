using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Model;

namespace Infrastructure.Data.Configurations.ValueObjects;

public static class ImageReferenceConfigurator
{
    public static void Configure<T>(OwnedNavigationBuilder<T, ImageReference> bld)
        where T : class
    {
        bld.Property(x => x.Url).HasMaxLength(SharedFieldLengths.URL);
        bld.Property(x => x.Width);
        bld.Property(x => x.Height);
    }
}
