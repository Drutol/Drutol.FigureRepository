using Drutol.FigureRepository.Shared.Models.Enums;
using Drutol.FigureRepository.Shared.Models.Figure;
using Microsoft.JSInterop;

namespace Drutol.FigureRepository.BlazorApp.Util;

public static class Extensions
{
    private static readonly Random Random = new Random();

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
        
    public static string Str(this FigureEyeType ev) => ev switch
    {
        FigureEyeType.None => "None",
        FigureEyeType.FullDecals => "Full Decals",
        FigureEyeType.IrisDecals => "Iris Decals",
        FigureEyeType.FullSculpted => "Full Sculpted",
        FigureEyeType.PartSculpted => "Partially Sculpted",
        _ => throw new ArgumentOutOfRangeException(nameof(ev), ev, null)
    };

    public static string TruncateAddress(this string str)
    {
        return $"{str.Substring(0, 5)}...{str.Substring(str.Length - 4, 4)}";
    }

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



    public static List<T> Shuffle<T>(this List<T> list)
    {
        var n = list.Count;
        while (n > 1)
        {
            n--;
            var k = Random.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }

        return list;
    }
}