﻿namespace Company.Videomatic.Application.Features.Playlists.Commands;

public record DeletePlaylistCommand(int Id) : DeleteAggregateRootCommand<Playlist>(Id);

internal class DeletePlaylistCommandValidator : AbstractValidator<DeletePlaylistCommand>
{
    public DeletePlaylistCommandValidator()
    {
       RuleFor(x => x.Id).GreaterThan(0);
    }
}