using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Videomatic.Application.Features.Videos.Commands.UpdateVideo;

public class UpdateVideoCommand : IRequest<UpdateVideoResponse>
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }

}