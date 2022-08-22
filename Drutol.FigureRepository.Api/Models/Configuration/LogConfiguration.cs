namespace Drutol.FigureRepository.Api.Models.Configuration
{
    public class LogConfiguration
    {
        public string LogDatabase { get; set; } = "Filename=DruFiguresLogs.db;Connection=shared";

        public string LogCollection { get; set; } = "logs";

        public string LogTemplate { get; set; } =
            "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message} {Exception} {Properties} {NewLine}";
    }
}
