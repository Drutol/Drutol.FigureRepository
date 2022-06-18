using Drutol.FigureRepository.BlazorApp.Models.Enums;

namespace Drutol.FigureRepository.BlazorApp.Util
{
    public static class Extensions
    {
        public static string ToCssValue(this FigurePhotoGravity gravity)
        {
            return gravity switch
            {
                FigurePhotoGravity.Center => "center",
                FigurePhotoGravity.Top => "top",
                FigurePhotoGravity.Bottom => "bottom",
                _ => throw new ArgumentOutOfRangeException(nameof(gravity), gravity, null)
            };
        }
    }
}
