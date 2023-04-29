using Company.SharedKernel.Specifications;
using Xunit.Abstractions;

namespace Company.Videomatic.Application.Tests.Features.Videos;

[Collection("Sequence")]
public partial class ApplicationTests
{
    public ApplicationTests(ITestOutputHelper output)
    {
        Output = output ?? throw new ArgumentNullException(nameof(output));
    }

    protected ITestOutputHelper Output { get; }
}
