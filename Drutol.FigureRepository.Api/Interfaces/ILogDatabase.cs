using Drutol.FigureRepository.Shared.Logs;
using Drutol.FigureRepository.Shared.Orders;
using Drutol.FigureRepository.Shared.Statistics;

namespace Drutol.FigureRepository.Api.Interfaces;

public interface ILogDatabase
{
    ValueTask<GetLogsRequestResult> GetLogs(GetLogsRequest request);
    ValueTask<LogStatistics> GetLogStatistics(GetStatisticsRequest request);
}