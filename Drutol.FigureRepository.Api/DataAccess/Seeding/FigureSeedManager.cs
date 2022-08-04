using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Shared.Models;

namespace Drutol.FigureRepository.Api.DataAccess.Seeding;

public class FigureSeedManager : IFigureSeedManager
{
    public Figure this[Guid index] => Figures.First(figure => figure.Guid == index);

    public List<Figure> Figures { get; } = new();
    private readonly IList<IFigureSeed> _seeds;

    public FigureSeedManager(IList<IFigureSeed> seeds)
    {
        _seeds = seeds;

        foreach (var seed in seeds)
        {
            Figures.Add(seed.Figure);
        }
    }
}