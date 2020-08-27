using System;
using System.Collections.Generic;

namespace CQRSAndEventSourcing.Application.SeedWork
{
    public class AggregateRoot : Entity
    {
        private List<DomainEvent> _domainEvents;

        public AggregateRoot(Guid id)
        {
            Id = id;
        }

        public AggregateRoot(IEnumerable<DomainEvent> domainEvents)
        {
            if (domainEvents == null)
                return;

            foreach (var domainEvent in domainEvents)
            {
                ApplyDomainEvent(domainEvent);
                Version++;
            }
        }

        public short Version { get; }

        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents;

        public void AddDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents = _domainEvents ?? new List<DomainEvent>();
            _domainEvents.Add(domainEvent);
        }

        private void ApplyDomainEvent(DomainEvent domainEvent) => ((dynamic)this).On((dynamic)domainEvent);
    }
}
