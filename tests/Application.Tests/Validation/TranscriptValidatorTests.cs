using Application.Tests.Helpers;

namespace Application.Tests.Validation;

public class TranscriptValidatorTests
{
    public ValidatorHelper ValidatorHelper { get; }

    public TranscriptValidatorTests(IServiceProvider serviceProvider)
    {
        ValidatorHelper = new ValidatorHelper(serviceProvider);
    }
}
