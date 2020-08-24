using System;
using System.Collections.Generic;

namespace CQRSAndEventSourcing.Application.SeedWork
{
    public class AggregateRoot : Entity
    {
        private List<DomainEvent> _domainEvents;

        public AggregateRoot(Guid id) : base(id)
        {
        }

        public short Version { get; }

        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents;

        public void AddDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents = _domainEvents ?? new List<DomainEvent>();
            _domainEvents.Add(domainEvent);
        }
    }
}
