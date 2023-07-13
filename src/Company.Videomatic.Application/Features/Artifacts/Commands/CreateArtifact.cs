﻿using Ardalis.Result;

namespace Company.Videomatic.Application.Features.Artifacts.Commands;

public record CreateArtifactCommand(long VideoId,
                                    string Name,
                                    string Type,
                                    string? Text) : IRequest<Result<CreateArtifactResponse>>;

public record CreateArtifactResponse(long Id);

public class CreateArtifactCommandValidator : AbstractValidator<CreateArtifactCommand>
{
    public CreateArtifactCommandValidator()
    {
        RuleFor(x => x.VideoId).GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Type).NotEmpty();
        RuleFor(x => x.Text).NotEmpty();
    }
}