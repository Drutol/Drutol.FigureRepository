using Drutol.FigureRepository.BlazorApp.Models.Enums;

namespace Drutol.FigureRepository.BlazorApp.Models
{
    public record FigureExternalLink
    {
        public string Url { get; set; }
        public FigureExternalLinkType Type { get; set; }
    }
}
