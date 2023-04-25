using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Domain;
using Company.Videomatic.Infrastructure.SemanticKernel.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;

namespace Company.Videomatic.Infrastructure.SemanticKernel;

public class SemanticKernelVideoAnalyzer : IVideoAnalyzer
{
    private readonly ILogger<SemanticKernelVideoAnalyzer> _logger;
    private readonly SemanticKernelOptions _options;
    private readonly IKernel _kernel;

    public SemanticKernelVideoAnalyzer(
        ILogger<SemanticKernelVideoAnalyzer> logger,
        IOptions<SemanticKernelOptions> options,
        IKernel kernel)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = options.Value;
        _kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));
    }

    public async Task<Artifact> SummarizeVideoAsync(Video video)
    {        
        var func = _kernel.CreateSemanticFunction("""
Write a summary of what the following transcript of a YouTube video discusses. 
The summary it should be no longer than 3 sentences and it will be used in a TL;DR section.

---Begin Text---
{{$INPUT}}
---End Text---
""", 
            maxTokens: 2000,
            temperature: 0.2,
            topP: 0.5);
        
        var myOutput = await _kernel.RunAsync(
            video.Transcripts.First().ToString(), // TODO: should account for all transcripts
            func);

        return new Artifact(title: "Summary", text: myOutput.ToString());
    }

    public async Task<Artifact> ReviewVideoAsync(Video video)
    {   
        var func = _kernel.CreateSemanticFunction("""
Write an extensive review of the following transcript of a YouTube video. Divide the review into three sections
and title each section as follows:  
1. A summary of the video.
2. A discussion of the video's strengths.
3. A discussion of the video's weaknesses.

---Begin Text---
{{$INPUT}}
---End Text---
""",
            maxTokens: 2000,
            temperature: 0.2,
            topP: 0.5);

        var myOutput = await _kernel.RunAsync(
            video.Transcripts.First().ToString(),// TODO: should account for all transcripts
            func);

        return new Artifact(title: "Review", myOutput.ToString());
    }
}