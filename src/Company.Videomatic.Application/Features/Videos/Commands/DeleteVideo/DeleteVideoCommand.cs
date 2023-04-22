using Company.Videomatic.Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Videomatic.Application.Features.Videos.Commands.DeleteVideo;

public class DeleteVideoCommand : IRequest<DeleteVideoResponse>
{
    public int VideoId { get; set; }

    public class DeleteVideoLinkHandler : IRequestHandler<DeleteVideoCommand, DeleteVideoResponse>
    {
        readonly IVideoStorage _repository;
        public DeleteVideoLinkHandler(IVideoStorage repository)
        {
            _repository = repository;
        }

        public async Task<DeleteVideoResponse> Handle(DeleteVideoCommand request, CancellationToken cancellationToken)
        {
            var res = await _repository.DeleteVideo(request.VideoId);
            
            return new DeleteVideoResponse 
            {
                Deleted = res
            };
        }
    }   
}