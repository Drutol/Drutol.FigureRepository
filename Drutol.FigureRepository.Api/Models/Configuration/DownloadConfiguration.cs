namespace Drutol.FigureRepository.Api.Models.Configuration
{
    public class DownloadConfiguration
    {
        public string ConnectionString { get; set; }
        public TimeSpan LinkExpirationTime { get; set; } = TimeSpan.FromMinutes(10);

        public Dictionary<string, string> Containers { get; set; }
    }
}
