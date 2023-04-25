using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Videomatic.Domain;

public class Artifact
{
    public int Id { get; init; }
    public required string Title { get; set; }
    public string? Text { get; set; }
    public int Size => Text?.Length ?? 0;

}