namespace Company.Videomatic.Application.Features.Videos.Queries;

public class GetTranscriptDTOQueryValidator : AbstractValidator<GetTranscriptDTOQuery>
{
    public GetTranscriptDTOQueryValidator()
    {
        RuleFor(x => x.TranscriptId).GreaterThan(0);        
    }
}