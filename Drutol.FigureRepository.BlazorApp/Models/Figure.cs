namespace Drutol.FigureRepository.BlazorApp.Models
{
    public class Figure
    {
        public string Name { get; set; }
        public string TokenAddress { get; set; }
        public string Description { get; set; }
        public string CgTraderLink { get; set; }

        public string PhotoUrl { get; set; }
        public string PhotoGravity { get; set; } = "center";

        public DateOnly PublishDate { get; set; }
    }
}
