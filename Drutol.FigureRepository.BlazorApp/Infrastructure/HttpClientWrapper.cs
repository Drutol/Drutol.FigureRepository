using Drutol.FigureRepository.BlazorApp.Interfaces;

namespace Drutol.FigureRepository.BlazorApp.Infrastructure
{
    public record HttpClientWrapper(HttpClient Client) : IHostHttpClient, IApiHttpClient;
}
