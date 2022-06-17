using Drutol.FigureRepository.BlazorApp.Models;

namespace Drutol.FigureRepository.BlazorApp.Infrastructure
{
    public class FigureProvider
    {
        public List<Figure> Figures { get; } = new()
        {
            new Figure
            {
                Name = "Ganyu",
                Description = "Ganyu from GenshinImpact based on her Banner Art.",
                PhotoUrl = "images/Ganyu.jpg",
                PhotoGravity = "top",
                PublishDate = new DateOnly(2021, 5, 1)
            },
            new Figure
            {
                Name = "Lumine",
                Description = "Lumine from GenshinImpact based on her scenic park collaboration artwork.",
                PhotoUrl = "images/Lumine.jpg",
                PhotoGravity = "top",
                PublishDate = new DateOnly(2021, 10, 22)
            },   
            new Figure
            {
                Name = "Asuka",
                Description = "Asuka from AoKana based on tapestry art in a different outfit.",
                PhotoUrl = "images/Asuka.jpg",
                PublishDate = new DateOnly(2022, 6, 17)
            },
        };
    }
}
