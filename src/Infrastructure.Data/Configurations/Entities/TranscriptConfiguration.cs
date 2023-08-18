using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace Infrastructure.Data.Configurations.Entities;

public abstract class TranscriptConfiguration : TrackedEntityConfiguration<Transcript>,  IEntityTypeConfiguration<Transcript>
{

    public override void Configure(EntityTypeBuilder<Transcript> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Id)
               .HasConversion(x => x.Value, y => new TranscriptId(y))
               .IsRequired(true);

        // ---------- Relationships ----------
        var valueComparer = new ValueComparer<IList<TranscriptLine>>(
            (c1, c2) => c1!.SequenceEqual(c2!),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToList());

        builder.Property(x => x.Lines)
               .HasConversion(x => JsonConvert.SerializeObject(x),
                              y => JsonConvert.DeserializeObject<TranscriptLine[]>(y) ?? Array.Empty<TranscriptLine>())
               .Metadata.SetValueComparer(valueComparer);        

        // ---------- Indices ----------
        builder.HasIndex(x => x.Language);
    }
}