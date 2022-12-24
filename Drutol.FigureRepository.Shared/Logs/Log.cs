using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Drutol.FigureRepository.Shared.Logs
{
    public record Log(DateTime DateTime, LogLevel Level, string Message, DruEventId? Id);
}
