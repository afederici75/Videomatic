namespace SharedKernel.CQRS.Commands;

internal sealed class Helpers
{ 
    public static TId GetIdPropertyValue<TUpdateCommand, TId>(TUpdateCommand request)
        where TId : struct
    {
        var idProp = typeof(TUpdateCommand).GetProperty("Id");
        if (idProp == null)
        {
            throw new InvalidOperationException("The command must have an Id property.");
        }

        TId id = (TId)idProp.GetValue(request)!;
        return id;
    }
}