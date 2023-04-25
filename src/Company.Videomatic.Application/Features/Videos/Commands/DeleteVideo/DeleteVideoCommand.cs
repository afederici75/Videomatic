namespace Company.Videomatic.Application.Features.Videos.Commands.DeleteVideo;

public class DeleteVideoCommand : IRequest<DeleteVideoResponse>
{
    public int VideoId { get; set; }

    public class DeleteVideoLinkHandler : IRequestHandler<DeleteVideoCommand, DeleteVideoResponse>
    {
        readonly IVideoStorage _repository;
        public DeleteVideoLinkHandler(IVideoStorage repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<DeleteVideoResponse> Handle(DeleteVideoCommand request, CancellationToken cancellationToken)
        {
            var res = await _repository.DeleteVideoAsync(request.VideoId);
            
            return new DeleteVideoResponse 
            {
                Deleted = res
            };
        }
    }   
}