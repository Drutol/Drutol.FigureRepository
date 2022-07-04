using Drutol.FigureRepository.Shared.Models;

namespace Drutol.FigureRepository.Api.Interfaces;

public interface IFigureSeedManager
{
    Figure this[Guid index] { get; }
    List<Figure> Figures { get; }
}