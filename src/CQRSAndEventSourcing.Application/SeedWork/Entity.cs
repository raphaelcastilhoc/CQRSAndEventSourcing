using System;

namespace CQRSAndEventSourcing.Application.SeedWork
{
    public abstract class Entity
    {
        public Entity(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
