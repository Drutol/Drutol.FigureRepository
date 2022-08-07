using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Api.Models.Configuration;
using Drutol.FigureRepository.Shared.Models.Enums;
using Drutol.FigureRepository.Shared.Models.Figure;
using Functional.Maybe;
using Microsoft.Extensions.Options;

namespace Drutol.FigureRepository.Api.Infrastructure.Downloads;

public class DownloadLinkGenerator : IDownloadLinkGenerator
{
    private readonly ILogger<DownloadLinkGenerator> _logger;
    private readonly IOptions<DownloadConfiguration> _config;

    public DownloadLinkGenerator(
        ILogger<DownloadLinkGenerator> logger,
        IOptions<DownloadConfiguration> config)
    {
        _logger = logger;
        _config = config;
    }

    public async ValueTask<Maybe<string>> GenerateDownloadTokenForFigure(
        Figure figure,
        FigureDownloadResource resource)
    {
        var client = new BlobServiceClient(_config.Value.ConnectionString);

        if (!_config.Value.Containers.TryGetValue(figure.Guid.ToString("D").ToUpper(), out var containerName))
        {
            _logger.LogError($"Missing container name for figure {figure.Name} ({figure.Guid})");
            return Maybe<string>.Nothing;
        }

        var containerClient = client.GetBlobContainerClient(containerName);

        var blobClient = containerClient.GetBlobClient(ResourceTypeToBlobName(resource));
        var uri = blobClient.GenerateSasUri(BlobSasPermissions.Read, DateTimeOffset.UtcNow.AddMinutes(10));

        return uri.ToString().ToMaybe();
    }

    private string ResourceTypeToBlobName(FigureDownloadResource resource) => resource.Type switch
    {
        FigureDownloadType.BlenderScene => "Blender.zip",
        FigureDownloadType.SlicedScenes => "Lychee.zip",
        FigureDownloadType.Stls => "Stl.zip",
        _ => throw new ArgumentOutOfRangeException()
    };
}