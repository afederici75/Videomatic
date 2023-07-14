﻿namespace Company.Videomatic.Application.Handlers.Videos.Commands;

public sealed class DeleteVideoHandler : DeleteAggregateRootHandler<DeleteVideoCommand, Video>
{
    public DeleteVideoHandler(IServiceProvider serviceProvider, IMapper mapper) : base(serviceProvider, mapper)
    {
    }
}