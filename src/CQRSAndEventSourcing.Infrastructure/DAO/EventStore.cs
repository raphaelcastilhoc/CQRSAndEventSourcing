using CQRSAndEventSourcing.Application.SeedWork;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace CQRSAndEventSourcing.Infrastructure.DAO
{
    public class EventStore : IEventStore
    {
        private readonly IMongoCollection<EventDto> _events;

        public EventStore(IMongoDatabase database)
        {
            _events = database.GetCollection<EventDto>("events");
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
            public EventDto(AggregateRoot aggregateRoot, DomainEvent @event)
            {
                Id = aggregateRoot.Id;
                Data = JsonConvert.SerializeObject(@event);
                CreatedAt = @event.CreatedAt;
                Version = aggregateRoot.Version + 1;
            }

            public Guid Id { get; private set; }

            public string Data { get; private set; }

            public DateTime CreatedAt { get; private set; }

            public int Version { get; private set; }
        }
    }
}
