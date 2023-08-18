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
    public void Validate<TCommand>(TCommand command, int expectedErrors)
        where TCommand : class
    {
        Type? validatorType = typeof(TCommand).GetNestedType("Validator", System.Reflection.BindingFlags.NonPublic);
        if (validatorType == null)
        {
            throw new InvalidOperationException($"Validator for {typeof(TCommand).Name} not found.");
        }
        // create an instance of the validator
        var validator = (IValidator<TCommand>?)Activator.CreateInstance(validatorType);
        var validation = validator.TestValidate(command);
        validation.Errors.Should().HaveCount(expectedErrors);
    }
}
