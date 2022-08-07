using Drutol.FigureRepository.Shared.Models.Enums;

namespace Drutol.FigureRepository.BlazorApp.Util
{
    public static class EnumToStringExtensions
    {
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
            FigureEyeType.FullSculpted => "Fully Sculpted",
            FigureEyeType.PartSculpted => "Partially Sculpted",
            _ => throw new ArgumentOutOfRangeException(nameof(ev), ev, null)
        };

        public static string Str(this FigureDownloadType ev) => ev switch
        {
            FigureDownloadType.BlenderScene => "Blender Scene",
            FigureDownloadType.SlicedScenes => "Lychee Scenes",
            FigureDownloadType.Stls => "Stls",
            _ => throw new ArgumentOutOfRangeException(nameof(ev), ev, null)
        };

        public static string Str(this FigureDownloadFileFormat ev) => ev switch
        {
            FigureDownloadFileFormat.Zip => "zip",
            _ => throw new ArgumentOutOfRangeException(nameof(ev), ev, null)
        };
    }
}
