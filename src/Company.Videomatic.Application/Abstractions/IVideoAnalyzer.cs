using Company.Videomatic.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Videomatic.Application.Abstractions;

public interface IVideoAnalyzer
{
    Task<Artifact> SummarizeVideo(Video video);
    Task<Artifact> ReviewVideo(Video video);
}