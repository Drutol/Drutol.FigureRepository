using Drutol.FigureRepository.Shared.Models.Figure;

namespace Drutol.FigureRepository.Api.Interfaces;

public interface IFigureSeed
{
    Figure Figure { get; }
}