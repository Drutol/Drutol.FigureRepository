using Drutol.FigureRepository.BlazorApp.Models.Enums;

namespace Drutol.FigureRepository.BlazorApp.Models
{
    public record FigureTimelineEntry
    {
        public DateOnly Date { get; init; }
        public FigureTimelineEvent Event { get; init; }
    }
}
