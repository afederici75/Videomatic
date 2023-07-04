namespace Company.Videomatic.Application.Features.Videos;

public enum ThumbnailResolutionDTO
{
    Default,
    Medium,
    High,
    Standard,
    MaxRes
}

public static class ThumbnailResolutionDTOExtensions
{
    public static ThumbnailResolution ToThumbnailResolution(this ThumbnailResolutionDTO instance)
    {
        switch (instance)
        {
            case ThumbnailResolutionDTO.Medium:
                return ThumbnailResolution.Medium;
            case ThumbnailResolutionDTO.High:
                return ThumbnailResolution.High;
            case ThumbnailResolutionDTO.Standard:
                return ThumbnailResolution.Standard;
            case ThumbnailResolutionDTO.MaxRes:
                return ThumbnailResolution.MaxRes;
            default:
                return ThumbnailResolution.Default;
        }
    }
}