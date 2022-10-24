using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drutol.FigureRepository.Shared.Models.Figure
{
    public record FigureMetadata
    {
        public string ImageUrl { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public string Url { get; init; }
        public string Type { get; init; }

        public int ImageWidth { get; init; }
        public int ImageHeight { get; init; }
        public string ImageMimeType { get; init; }
    }
}
