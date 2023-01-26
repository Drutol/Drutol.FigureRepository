using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drutol.FigureRepository.Shared.Util;

public static class KnownFigureUtils
{
    public static Dictionary<KnownFigures, Guid> Guids = new()
    {
        { KnownFigures.Asuka, Guid.Parse("DF7449B8-4915-487A-A244-0E2E4C0B55E2") },
        { KnownFigures.Ganyu, Guid.Parse("046BA215-AE76-44E4-BB14-01BBEAB30527") },
        { KnownFigures.Lumine, Guid.Parse("E46284DB-90D7-4A2B-BA97-36C599C1FA87") },
        { KnownFigures.Madoka, Guid.Parse("8EE2F4AC-40F9-4FD7-A551-FE679A015ED7") }
    };
}
