﻿using SharedKernel.CQRS.Commands;

namespace Application.Features.Playlists.Commands;

public readonly record struct CreatePlaylistCommand(string Name,
                                    string? Description = null) : IRequest<Result<Playlist>>;

internal class CreatePlaylistCommandValidator : AbstractValidator<CreatePlaylistCommand>
{
    public CreatePlaylistCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();        
    }
}