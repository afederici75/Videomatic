namespace Company.Videomatic.SemanticKernel;

using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Domain;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.KernelExtensions;
using Microsoft.SemanticKernel.Orchestration;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SemanticKernelVideoAnalyzer : IVideoAnalyzer
{
    private readonly ILogger<SemanticKernelVideoAnalyzer> _logger;
    private readonly IKernel _kernel;
    private readonly IDictionary<string, ISKFunction> _transcriptSkill;

    public SemanticKernelVideoAnalyzer(
        ILogger<SemanticKernelVideoAnalyzer> logger,
        IKernel kernel)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));
        _transcriptSkill = _kernel.ImportSemanticSkillFromDirectory("Skills", "TranscriptSkill");
    }

    public async Task<string> SummarizeTranscript(Transcript transcript)
    {
        var myContext = new ContextVariables();
        myContext.Set("input", transcript.ToString());

        var myResult = await _kernel.RunAsync(myContext, _transcriptSkill["Summarize"]);

        return myResult.ToString();
    }
}