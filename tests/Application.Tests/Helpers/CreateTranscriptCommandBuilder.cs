using Application.Features.Transcripts.Commands;
using Domain.Videos;

namespace Application.Tests.Helpers;

public class CreateTranscriptCommandBuilder
{
    public static CreateTranscriptCommand WithDummyValues(VideoId videoId)
    {
        return new CreateTranscriptCommand(VideoId: videoId,
                                           Language: "US",
                                           Lines: new[] 
                                           { 
                                               "Line1" ,
                                               "Line2"
                                           });        
    }
}