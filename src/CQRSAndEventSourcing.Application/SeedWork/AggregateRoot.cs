using System;
using System.Collections.Generic;

namespace CQRSAndEventSourcing.Application.SeedWork
{
    public abstract class AggregateRoot : Entity
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

            ApplyDomainEvents(domainEvents);
            Version++;
        }

        public short Version { get; private set; }

        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents;

        public void AddDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents = _domainEvents ?? new List<DomainEvent>();
            _domainEvents.Add(domainEvent);
        }

        public void ApplyDomainEvents(IEnumerable<DomainEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
            {
                ((dynamic)this).On((dynamic)domainEvent);
                Version++;
            }
        }

        //private void ApplyDomainEvent(DomainEvent domainEvent) => ((dynamic)this).On((dynamic)domainEvent);
    }
}
