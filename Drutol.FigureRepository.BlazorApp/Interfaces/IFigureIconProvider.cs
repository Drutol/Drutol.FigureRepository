using Drutol.FigureRepository.Shared.Models;

namespace Drutol.FigureRepository.BlazorApp.Interfaces;

public interface IFigureIconProvider
{
    string AsukaIcon { get; }
    string GanyuIcon { get; }
    string LumineIcon { get; }
    string MadokaIcon { get; }
    string FigureCharacterIcon { get; }
    string GetForFigure(Figure figure);
}