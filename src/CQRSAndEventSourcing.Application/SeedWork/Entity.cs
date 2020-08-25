using System;

namespace CQRSAndEventSourcing.Application.SeedWork
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
    }
}
