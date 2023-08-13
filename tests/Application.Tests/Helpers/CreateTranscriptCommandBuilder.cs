using Company.Videomatic.Application.Features.Transcripts.Commands;
using Company.Videomatic.Domain.Video;

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