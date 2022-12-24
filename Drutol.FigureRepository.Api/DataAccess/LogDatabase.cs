using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Api.Models.Checkout;
using Drutol.FigureRepository.Api.Models.Configuration;
using Drutol.FigureRepository.Shared.Logs;
using Drutol.FigureRepository.Shared.Orders;
using Drutol.FigureRepository.Shared.Statistics;
using LiteDB;
using Microsoft.Extensions.Options;

namespace Drutol.FigureRepository.Api.DataAccess
{
    public class LogDatabase : ILogDatabase
    {
        private readonly IOptions<LogConfiguration> _config;
        private readonly LiteDatabase _database;
        private readonly Dictionary<DruEventId, string> _eventIds;

        public LogDatabase(IOptions<LogConfiguration> config)
        {
            _config = config;
            _database = new LiteDatabase(config.Value.LogDatabase);

            _eventIds = Enum.GetValues<DruEventId>().ToDictionary(id => id, id => id.ToString());

            var logs = _database.GetCollection(_config.Value.LogCollection);
            logs.EnsureIndex(entity => entity["_t"]);
            logs.EnsureIndex(entity => entity["_l"]);
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
                DruEventId? eventId = null;
                if (document.ContainsKey("EventId"))
                {
                    var id = (DruEventId)document["EventId"].AsDocument["Id"].AsInt32;
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

        public ValueTask<LogStatistics> GetLogStatistics(GetStatisticsRequest request)
        {
            ILiteQueryable<BsonDocument> BuildQuery()
            {
                var liteQueryable = _database.GetCollection(_config.Value.LogCollection).Query();

                if (request.From is { })
                    liteQueryable = liteQueryable.Where(entity => entity["_t"].AsDateTime >= request.From);

                if (request.To is { })
                    liteQueryable = liteQueryable.Where(entity => entity["_t"].AsDateTime <= request.To);
                return liteQueryable;
            }

            var eventCounts = new Dictionary<DruEventId, int>();
            var levelCounts = new Dictionary<LogLevel, int>();

            foreach (var eventId in Enum.GetValues<DruEventId>())
            {
                var filter = new BsonDocument(new Dictionary<string, BsonValue>
                {
                    { "Id", new BsonValue((int)eventId) },
                    { "Name", new BsonValue(eventId.ToString()) }
                });

                eventCounts[eventId] = BuildQuery().Where(entity => entity["EventId"] == filter).Count();
            } 
            
            foreach (var level in Enum.GetValues<LogLevel>())
            {
                levelCounts[level] = BuildQuery().Where(entity => entity["_l"].AsString == level.ToString()).Count();
            }

            return ValueTask.FromResult<LogStatistics>(new(levelCounts, eventCounts));
        }
    }
}
