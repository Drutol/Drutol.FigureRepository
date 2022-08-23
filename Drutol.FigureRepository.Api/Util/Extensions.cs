using Drutol.FigureRepository.Api.Logging;

namespace Drutol.FigureRepository.Api.Util
{
    public static class Extensions
    {
        public static EventId Ev(this DruEventId id) => new((int)id, id.ToString());
    }
}
