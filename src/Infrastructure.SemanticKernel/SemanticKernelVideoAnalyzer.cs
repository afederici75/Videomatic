//using Application.Abstractions;
//using Domain.Model;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using Microsoft.SemanticKernel;

//namespace Infrastructure.SemanticKernel;

//public class SemanticKernelVideoAnalyzer : IVideoAnalyzer
//{
//    private const int MaxtTextLength = 8000;

//    private readonly ILogger<SemanticKernelVideoAnalyzer> _logger;
//    private readonly SemanticKernelOptions _options;
//    private readonly IKernel _kernel;

//    public SemanticKernelVideoAnalyzer(
//        ILogger<SemanticKernelVideoAnalyzer> logger,
//        IOptions<SemanticKernelOptions> options,
//        IKernel kernel)
//    {
//        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
//        _options = options.Value;
//        _kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));
//    }

//    // TODO: find a better way
//    static string GetMaxTextTEMP(string? input)
//    {
//        if (string.IsNullOrEmpty(input))
//            return string.Empty;

//        if (input.Length > MaxtTextLength)
//        {
//            return input.Substring(0, MaxtTextLength);
//        }

//        return input;
//    }

//    public async Task<Artifact> SummarizeVideoAsync(Video video)
//    {        
//        var func = _kernel.CreateSemanticFunction("""
//Write a summary of what the following transcript of a YouTube video discusses. 
//The summary it should be no longer than 3 sentences and it will be used in a TL;DR section.

//---Begin Text---
//{{$INPUT}}
//---End Text---
//""", 
//            maxTokens: 2000,
//            temperature: 0.2,
//            topP: 0.5);

//        var transcript = GetMaxTextTEMP(video.Transcripts?.FirstOrDefault()?.ToString() ?? string.Empty);// TODO: should account for all transcripts                       
//        var myOutput = await _kernel.RunAsync(transcript, func);

//        return new Artifact(title: "Summary", type: "AI", text: myOutput.ToString());
//    }

//    public async Task<Artifact> ReviewVideoAsync(Video video)
//    {
//        var func = _kernel.CreateSemanticFunction("""
//Write an extensive review of the following transcript of a YouTube video. Divide the review into three sections
//and title each section as follows:  
//1. A summary of the video.
//2. A discussion of the video's strengths.
//3. A discussion of the video's weaknesses.

//---Begin Text---
//{{$INPUT}}
//---End Text---
//""",
//            maxTokens: 2000,
//            temperature: 0.2,
//            topP: 0.5);

//        var transcript = GetMaxTextTEMP(video.Transcripts?.FirstOrDefault()?.ToString() ?? string.Empty);// TODO: should account for all transcripts       
//        var myOutput = await _kernel.RunAsync(transcript, func);

//        return new Artifact(title: "Review", myOutput.ToString());
//    }
//}