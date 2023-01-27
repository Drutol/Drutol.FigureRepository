using Drutol.FigureRepository.Shared.Models.Figure;

namespace Drutol.FigureRepository.BlazorApp.Interfaces.Figures;

public interface IFigureIconProvider
{
    string AsukaIcon { get; }
    string GanyuIcon { get; }
    string LumineIcon { get; }
    string MadokaIcon { get; }
    string FigureCharacterIcon { get; }
    string FigureOriginIcon { get; }
    string GetForFigure(Figure figure);
}