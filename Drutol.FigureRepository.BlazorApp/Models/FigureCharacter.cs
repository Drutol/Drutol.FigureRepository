namespace Drutol.FigureRepository.BlazorApp.Models
{
    public record FigureCharacter
    {
        public string NameEnglish { get; init; }
        public string NameJapanese { get; init; }
        public string OriginNameEnglish { get; set; }
        public string OriginNameJapanese { get; set; }
    }
}
