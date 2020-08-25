using CQRSAndEventSourcing.Application.SeedWork;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSAndEventSourcing.Infrastructure.DAO
{
    public class EventStore : IEventStore
    {
        private readonly IMongoCollection<EventDto> _events;

        private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All,
            NullValueHandling = NullValueHandling.Ignore
        };

        public EventStore(IMongoDatabase database)
        {
            _events = database.GetCollection<EventDto>("events");
        }

        public async Task<IEnumerable<DomainEvent>> GetAsync(Guid aggregateId)
        {
            var events = await _events.Find(x => x.AggregateId == aggregateId.ToString()).ToListAsync();
            var domainEvents = events.Select(TransformEvent);

            return domainEvents;
        }

        private DomainEvent TransformEvent(EventDto eventDto)
        {
            var deserializedObject = JsonConvert.DeserializeObject(eventDto.Data, _jsonSerializerSettings);
            var domainEvent = deserializedObject as DomainEvent;

            return domainEvent;
        }

        public async Task SaveAsync(AggregateRoot aggregateRoot)
        {
            foreach (var @event in aggregateRoot.DomainEvents)
            {
                var eventDto = new EventDto(aggregateRoot, @event);
                await _events.InsertOneAsync(eventDto);
            }
        }

        private class EventDto
        {
            private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
                NullValueHandling = NullValueHandling.Ignore
            };

            public EventDto(AggregateRoot aggregateRoot, DomainEvent @event)
            {
                AggregateId = aggregateRoot.Id.ToString();
                Data = JsonConvert.SerializeObject(@event, Formatting.Indented, _jsonSerializerSettings);
                CreatedAt = @event.CreatedAt;
                Version = aggregateRoot.Version + 1;
            }

            public string AggregateId { get; private set; }

            public string Data { get; private set; }

            public DateTime CreatedAt { get; private set; }

            public int Version { get; private set; }
        }
    }
}
