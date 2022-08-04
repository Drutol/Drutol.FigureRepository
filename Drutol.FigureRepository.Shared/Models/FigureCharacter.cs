namespace Drutol.FigureRepository.Shared.Models;

public record FigureCharacter
{
    public string NameEnglish { get; init; }
    public string NameJapanese { get; init; }
    public string OriginNameEnglish { get; set; }
    public string OriginNameJapanese { get; set; }

    public string CharacterUrl { get; set; }
    public string OriginUrl { get; set; }
}