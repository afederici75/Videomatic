using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace Infrastructure.Data.Extensions;


public interface IStronglyTypedIdConverter
{
    //public Type ValueConverter { get; }
}

public class VideoIdConverter : ValueConverter<VideoId, int>, IStronglyTypedIdConverter
{
    public VideoIdConverter()
        : base(
            id => id.Value,
            value => new VideoId(value)            
        )
    { }
}

public class PlaylistIdConverter : ValueConverter<PlaylistId, int>, IStronglyTypedIdConverter
{
    public PlaylistIdConverter()
        : base(id => id.Value,
               value => new PlaylistId(value))
    { }
}

public class ArtifactIdConverter : ValueConverter<ArtifactId, int>, IStronglyTypedIdConverter
{
    public ArtifactIdConverter()
        : base(id => id.Value,
               value => new ArtifactId(value))
    { }
}

public class TranscriptIdConverter : ValueConverter<TranscriptId, int>, IStronglyTypedIdConverter
{
    public TranscriptIdConverter()
        : base(id => id.Value,
               value => new TranscriptId(value))
    { }
}

public static class ModelBuilderExtensions
{
    public static ModelBuilder AddStronglyTypedIdValueConverters(
        this ModelBuilder modelBuilder, Assembly assembly)
    {
        var targets = assembly.GetTypes()
            .Where(t => t.IsClass && 
                        !t.IsAbstract &&
                        (typeof(IStronglyTypedIdConverter).IsAssignableFrom(t)) && 
                        (typeof(ValueConverter).IsAssignableFrom(t)))
            .ToArray(); 

        foreach (Type type in targets)
        {
            var typeBase = type.BaseType;
            if ((typeBase == null) || !typeBase.IsGenericType)
            {
                continue;
            }

            var args = typeBase.GetGenericArguments();
            
            var stronglyTypedIdType = args[0];
            var convertedType = args[1];


            // The ValueConverter must have a parameterless constructor
            var converter = (ValueConverter)Activator.CreateInstance(type)!; // CreateInstance throws

            // Register the value converter for all EF Core properties that use the ID
            modelBuilder.UseValueConverter(converter);
        }

        return modelBuilder;
    }

    // This method is the same as shown previously
    public static ModelBuilder UseValueConverter(
        this ModelBuilder modelBuilder, ValueConverter converter)
    {
        var type = converter.ModelClrType;

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var properties = entityType
                .ClrType
                .GetProperties()
                .Where(p => p.PropertyType == type);

            foreach (var property in properties)
            {
                modelBuilder
                    .Entity(entityType.Name)
                    .Property(property.Name)
                    .HasConversion(converter);
            }
        }

        return modelBuilder;
    }
}