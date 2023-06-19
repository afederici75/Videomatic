namespace Company.Videomatic.Application.Features.Model;

public record ThumbnailDTO(
    long Id = 0,
    long VideoId = 0,
    string Location = "",
    ThumbnailResolution Resolution = ThumbnailResolution.Default,
    int Height = 0,
    int Width = 0);