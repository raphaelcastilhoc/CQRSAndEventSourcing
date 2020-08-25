using System;

namespace CQRSAndEventSourcing.Application.SeedWork
{
    public abstract class DomainEvent
    {
        public DomainEvent()
        {
            CreatedAt = DateTime.Now;
        }

        public DateTime CreatedAt { get; private set; }
    }
}
