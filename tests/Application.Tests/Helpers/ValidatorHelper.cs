using System.Diagnostics;

namespace Application.Tests.Helpers;

public class ValidatorHelper
{
    public ValidatorHelper(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public IServiceProvider ServiceProvider { get; }

    [DebuggerStepThrough]
    public void Validate<TVALIDATOR, TREQUEST>(TREQUEST request, int expectedErrors)
            where TVALIDATOR : IValidator<TREQUEST>
    {
        // This way if the validator's ctor has parameters they will get resolved.
        var validator = ServiceProvider.GetService<TVALIDATOR>();
        var validation = validator.TestValidate(request);
        validation.Errors.Should().HaveCount(expectedErrors);
    }
}
