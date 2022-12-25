using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drutol.FigureRepository.Shared.Logs;
using Microsoft.Extensions.Logging;

namespace Drutol.FigureRepository.Shared.Statistics;

public record LogStatistics(Dictionary<LogLevel, int> LogLevelCounts, Dictionary<DruEventId, int> EventCounts);