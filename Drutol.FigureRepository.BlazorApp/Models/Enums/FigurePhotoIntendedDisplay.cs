namespace Drutol.FigureRepository.BlazorApp.Models.Enums
{
    [Flags]
    public enum FigurePhotoIntendedDisplay
    {
        Gallery = 0,
        FigureIndex = 1 << 1,
        HomeShowcase = 1 << 2,
        FigureDetails = 1 << 3,

        All = Gallery | FigureIndex | HomeShowcase | FigureDetails
    }
}
