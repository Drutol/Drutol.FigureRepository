using Drutol.FigureRepository.Shared.Models;

namespace Drutol.FigureRepository.BlazorApp.Interfaces;

public interface IFigureProvider
{
    TaskCompletionSource DataReady { get; }
    List<Figure> Figures { get; }
    ValueTask Initialize();
}