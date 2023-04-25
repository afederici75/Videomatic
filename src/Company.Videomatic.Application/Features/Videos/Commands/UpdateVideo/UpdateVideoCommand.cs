using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Domain;
using MediatR;

namespace Company.Videomatic.Application.Features.Videos.Commands.UpdateVideo;

public class UpdateVideoCommand : IRequest<UpdateVideoResponse>
{
    public int VideoId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }


    public class UpdateVideoCommandHandler : IRequestHandler<UpdateVideoCommand, UpdateVideoResponse>
    {
        private readonly IVideoStorage _storage;
        
        public UpdateVideoCommandHandler(IVideoStorage videoStorage)
        {
            _storage = videoStorage;        
        }

        public async Task<UpdateVideoResponse> Handle(UpdateVideoCommand request, CancellationToken cancellationToken)
        {
            Video? video = await _storage.GetVideoByIdAsync(request.VideoId, VideoQueryOptions.Default);
            if (video is null)
                return new UpdateVideoResponse { Updated = false };

            video.Title = request.Title;
            video.Description = request.Description;

            var res = await _storage.UpdateVideoAsync(video);

            return new UpdateVideoResponse { Updated = res > 0 };
        }
    }
}