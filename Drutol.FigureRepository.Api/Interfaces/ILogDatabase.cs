using Drutol.FigureRepository.Shared.Logs;
using Drutol.FigureRepository.Shared.Orders;

namespace Drutol.FigureRepository.Api.Interfaces;

public interface ILogDatabase
{
    ValueTask<GetLogsRequestResult> GetLogs(GetLogsRequest request);
}