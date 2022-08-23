using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drutol.FigureRepository.Shared.Statistics
{
    public record GetStatisticsRequest
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }

    public record GetStatisticsRequestResult(Dictionary<Guid, FigureStatistics> FigureStatistics, LogStatistics LogStatistics);
}
