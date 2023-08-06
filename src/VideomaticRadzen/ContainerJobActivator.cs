using Hangfire;
using System.ComponentModel;

namespace VideomaticRadzen;

public class ContainerJobActivator : JobActivator
{
    readonly IServiceProvider Provider;

    public ContainerJobActivator(IServiceProvider provider)
    {
        Provider = provider ?? throw new ArgumentNullException(nameof(provider));
    }

    public override object ActivateJob(Type type)
    {
        return Provider.GetRequiredService(type);
    }
}