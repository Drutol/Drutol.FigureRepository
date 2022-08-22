using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Api.Logging;
using Drutol.FigureRepository.Api.Models.Configuration;
using Drutol.FigureRepository.Shared.Logs;
using Drutol.FigureRepository.Shared.Orders;
using LiteDB;
using Microsoft.Extensions.Options;

namespace Drutol.FigureRepository.Api.DataAccess
{
    public class LogDatabase : ILogDatabase
    {
        private readonly IOptions<LogConfiguration> _config;
        private readonly LiteDatabase _database;
        private readonly Dictionary<EventIds, string> _eventIds;

        public LogDatabase(IOptions<LogConfiguration> config)
        {
            _config = config;
            _database = new LiteDatabase(config.Value.LogDatabase);

            _eventIds = Enum.GetValues<EventIds>().ToDictionary(id => id, id => id.ToString());
        }

        public ValueTask<GetLogsRequestResult> GetLogs(GetLogsRequest request)
        {
            var collection = _database.GetCollection(_config.Value.LogCollection);
            var query = collection.Query().Include(document => document["EventId"]);

            if (request.LogLevels is { Count: > 0 })
            {
                var levelStrings = request.LogLevels.Select(x => x.ToString()).ToList();
                query = query.Where(entity => levelStrings.Contains(entity["_l"].AsString));
            }

            if (request.EventIds is { Count: > 0 })
            {
                var documents = request.EventIds.Select(id => new BsonDocument(new Dictionary<string, BsonValue>
                {
                    { "Id", new BsonValue((int)id) },
                    { "Name", new BsonValue(id.ToString()) }
                })).ToList();
                query = query.Where(entity => documents.Contains(entity["EventId"]));
            }

            if (request.From is { })
                query = query.Where(entity => entity["_t"].AsDateTime >= request.From);

            if (request.To is { })
                query = query.Where(entity => entity["_t"].AsDateTime <= request.To);

            var totalCount = query.Count();

            if (request.Take == 0)
                request.Take = 100;

            var logDocuments = query.Limit(Math.Min(100, request.Take)).Offset(request.Skip).ToList();
            var logs = logDocuments.Select(document =>
            {
                EventIds? eventId = null;
                if (document.ContainsKey("EventId"))
                {
                    var id = (EventIds)document["EventId"].AsDocument["Id"].AsInt32;
                    var name = document["EventId"].AsDocument["Name"].AsString;

                    if (_eventIds.TryGetValue(id, out var eventName) && eventName == name)
                        eventId = id;
                }

                return new Log(
                    document["_t"].AsDateTime,
                    Enum.Parse<LogLevel>(document["_l"].AsString),
                    document["_m"].AsString,
                    eventId);
            }).ToList();

            return ValueTask.FromResult(new GetLogsRequestResult(totalCount, logs));
        }
    }
}
