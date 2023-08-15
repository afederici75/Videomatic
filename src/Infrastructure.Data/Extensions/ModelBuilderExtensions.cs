using Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection;

namespace Infrastructure.Data.Extensions;

// Greatly inspired by
// https://andrewlock.net/strongly-typed-ids-in-ef-core-using-strongly-typed-entity-ids-to-avoid-primitive-obsession-part-4/#creating-a-custom-valueconverterselector-for-strongly-typed-ids

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

            // The IStronglyTypedIdConverter must have a parameterless constructor
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