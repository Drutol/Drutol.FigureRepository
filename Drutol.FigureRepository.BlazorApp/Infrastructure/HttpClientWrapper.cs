using Drutol.FigureRepository.BlazorApp.Interfaces.Http;

namespace Drutol.FigureRepository.BlazorApp.Infrastructure;

public record HttpClientWrapper(HttpClient Client) : IHostHttpClient, IApiHttpClient, ILoopringHttpClient;