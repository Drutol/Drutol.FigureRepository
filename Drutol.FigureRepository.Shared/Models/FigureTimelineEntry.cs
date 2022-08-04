using System;
using Drutol.FigureRepository.Shared.Models.Enums;

namespace Drutol.FigureRepository.Shared.Models;

public record FigureTimelineEntry
{
    public DateOnly Date { get; init; }
    public FigureTimelineEvent Event { get; init; }
}