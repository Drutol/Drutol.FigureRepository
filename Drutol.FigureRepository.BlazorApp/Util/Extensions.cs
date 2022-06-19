using Drutol.FigureRepository.BlazorApp.Models;
using Drutol.FigureRepository.BlazorApp.Models.Enums;
using Microsoft.JSInterop;

namespace Drutol.FigureRepository.BlazorApp.Util
{
    public static class Extensions
    {
        public static void OpenGallery(this IEnumerable<FigureMedia> media, IJSRuntime js, int index)
        {
            js.InvokeVoidAsync("openLightBox",
                media.Select(m => new
                {
                    src = m.Url,
                    width = m.Width,
                    height = m.Height,
                }).ToArray(),
                index);
        }

        public static string Str(this FigureTimelineEvent ev) => ev switch
        {
            FigureTimelineEvent.ProjectInception => "Inception",
            FigureTimelineEvent.SculptDone => "Sculpted",
            FigureTimelineEvent.PrintDone => "Printed",
            FigureTimelineEvent.PaintDone => "Painted",
            FigureTimelineEvent.Publish => "Published",
            _ => throw new ArgumentOutOfRangeException(nameof(ev), ev, null)
        };  
        
        public static string Str(this FigureMediaKind ev) => ev switch
        {
            FigureMediaKind.ShadedRender => "Render",
            FigureMediaKind.SculptRender => "Sculpt Render",
            FigureMediaKind.PrintPrototype => "Prototype",
            FigureMediaKind.WorkInProgress => "WIP",
            FigureMediaKind.Painted => "Painted",
            _ => throw new ArgumentOutOfRangeException(nameof(ev), ev, null)
        };

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
