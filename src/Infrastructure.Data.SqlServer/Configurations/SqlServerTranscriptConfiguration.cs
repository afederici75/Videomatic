﻿using Infrastructure.Data.Configurations.Entities;

namespace Infrastructure.SqlServer.Configurations;

public class SqlServerTranscriptConfiguration : TranscriptConfiguration
{
    public const string SequenceName = "TranscriptSequence";
    public const string TranscriptLineSequenceName = "TranscriptLineSequence";

    public override void Configure(EntityTypeBuilder<Transcript> builder)
    {
        base.Configure(builder);
        
        builder.Property(x => x.Id)
               .HasDefaultValueSql($"NEXT VALUE FOR {SequenceName}"); // TODO: unhardcode

        var thumbnails = builder.OwnsMany(x => x.Lines,
            (builder) =>
            {                
                builder.Property("Id")
                       .HasDefaultValueSql($"NEXT VALUE FOR {TranscriptLineSequenceName}"); // TODO: unhardcode                
            });
    }
}   