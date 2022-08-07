using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drutol.FigureRepository.Shared.Models.Enums;
using Drutol.FigureRepository.Shared.Models.Figure;

namespace Drutol.FigureRepository.Shared.Download;

public record DownloadFigureRequest(Guid FigureGuid, string Token);

public record DownloadFigureResponse(bool Success, Dictionary<FigureDownloadType, string> DownloadUrls = null);