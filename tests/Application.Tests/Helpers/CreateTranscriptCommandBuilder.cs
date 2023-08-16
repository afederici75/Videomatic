using Application.Features.Transcripts.Commands;
using Domain.Videos;

namespace Application.Tests.Helpers;

public class CreateTranscriptCommandBuilder
{
    public static CreateTranscriptCommand WithDummyValues(VideoId videoId)
    {
        return new CreateTranscriptCommand(videoId: videoId,
                                           language: "US",
                                           lines: new[] 
                                           { 
                                               "Line1" ,
                                               "Line2"
                                           });        
    }
}