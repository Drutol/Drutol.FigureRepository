using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Drutol.FigureRepository.Shared.Logs
{
    public record GetLogsRequest
    {
        public List<LogLevel> LogLevels { get; set; }
        public List<DruEventId> EventIds { get; set; }

        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        public int Take { get; set; }
        public int Skip { get; set; }
    }

    public record GetLogsRequestResult(int TotalCount, List<Log> Logs);
}
