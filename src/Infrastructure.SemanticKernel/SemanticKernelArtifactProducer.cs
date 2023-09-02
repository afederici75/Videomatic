using Application.Abstractions;
using Application.Specifications;
using Domain.Artifacts;
using Domain.Transcripts;
using Domain.Videos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Text;
using Newtonsoft.Json;
using SharedKernel.Abstractions;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace Infrastructure.SemanticKernel;

public class SemanticKernelArtifactProducer : IArtifactProducer
{
    private const int MaxtTextLength = 8000;

    private readonly ILogger<SemanticKernelArtifactProducer> _logger;
    private readonly IReadRepository<Video> _videoRepository;
    private readonly IReadRepository<Transcript> _transRepository;
    private readonly SemanticKernelOptions _options;
    private readonly IKernel _kernel;

    public SemanticKernelArtifactProducer(
        ILogger<SemanticKernelArtifactProducer> logger,
        IReadRepository<Video> videoRepository,
        IReadRepository<Transcript> transRepository,
        IOptions<SemanticKernelOptions> options,
        IKernel kernel)
    {        
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _videoRepository = videoRepository ?? throw new ArgumentNullException(nameof(videoRepository));
        _transRepository = transRepository ?? throw new ArgumentNullException(nameof(transRepository));
        _options = options.Value;
        _kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));    
    }

    public async Task CreateEncodings(VideoId videoId, CancellationToken cancellationToken)
    {
        var video = await _videoRepository.SingleOrDefaultAsync(new QueryVideos.ById(videoId), cancellationToken);    
        if (video == null)
        {
            return;
        }

        var fullText = await GetTranscriptOfVideo(video.Id, cancellationToken);

        if (fullText == null)
        {
            // TODO: log
            return;
        }

        // TODO: remove harccoded provider
        await CreateEmbeddings("YOUTUBE", video.Origin.ProviderItemId, fullText, cancellationToken);
    }
    
    const int ChunkSize = 200;  

    public async Task CreateEmbeddings(
        string providerId,
        string providerItemId,
        string fullTranscriptText, 
        CancellationToken cancellationToken)
    {        

        // TODO: does this chunking work well?
        List<string> chunkTexts = TextChunker.SplitPlainTextLines(fullTranscriptText, ChunkSize);

        // -index each chunk using a 
        // include timestamp of the chunk relative to the video!!!
        var idx = 0;
        foreach (var chunkText in chunkTexts)
        {
            // TODO: https://github.com/microsoft/semantic-kernel/discussions/2689

            // YOUTUBE:B3dsajQ2:0
            var idtxt = $"{providerId}:{providerItemId}:{idx}";

            // Chunk 1/3[200] of https://www.youtube.com/watch?v=324Bdsjkh32
            var desctxt = $"Chunk {idx + 1}/{chunkTexts.Count}[{ChunkSize}] of video https://www.youtube.com/watch?v={providerItemId}";

            var res2 = await _kernel.Memory.SaveReferenceAsync(                
                externalSourceName: "YOUTUBE",
                collection: "Videos",
                text: chunkText,                
                externalId: idtxt,
                description: desctxt,
                additionalMetadata: null);
            
            idx++;
        }

        // now we can do basic semantic search on the video       
    }

    async Task<string?> GetTranscriptOfVideo(VideoId videoId, CancellationToken cancellationToken)
    {
        // -get the transcript of the video using repository                
        var transOfVid = await _transRepository.ListAsync(new QueryTranscripts.ByVideoId(videoId));
        if (!transOfVid.Any())
            return null;

        // We try to find the first transcripton in English.
        // In the future we could start from the one that is equal to the user's language.

        var trans = transOfVid.FirstOrDefault(t => t.Language.StartsWith("EN", StringComparison.OrdinalIgnoreCase));
        if (trans == null) 
            return null;

        return trans.GetFullText();
    }


    #region OLD SK Code
    // TODO: find a better way
    static string GetMaxTextTEMP(string? input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        if (input.Length > MaxtTextLength)
        {
            return input.Substring(0, MaxtTextLength);
        }

        return input;
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

        throw new NotImplementedException();

        //var transcript = GetMaxTextTEMP(video.Transcripts?.FirstOrDefault()?.ToString() ?? string.Empty);// TODO: should account for all transcripts                       
        //var myOutput = await _kernel.RunAsync(transcript, func);
        //
        //return new Artifact(title: "Summary", type: "AI", text: myOutput.ToString());
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

        throw new NotImplementedException();

        //var transcript = GetMaxTextTEMP(video.Transcripts?.FirstOrDefault()?.ToString() ?? string.Empty);// TODO: should account for all transcripts       
        //var myOutput = await _kernel.RunAsync(transcript, func);
        //
        //return new Artifact(title: "Review", myOutput.ToString());
    }
    #endregion

}