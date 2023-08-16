using Application.Features.Transcripts.Commands;
using Domain.Transcripts;
using Domain.Videos;

namespace Application.Tests.Helpers;

public class CreateTranscriptCommandBuilder
{
    public static UpdateTranscriptCommand WithDummyValues(TranscriptId transcriptId)
    {
        return new UpdateTranscriptCommand(transcriptId: transcriptId,
                                           language: "US",
                                           lines: new[] 
                                           { 
                                               "Line1" ,
                                               "Line2"
                                           });        
    }
}