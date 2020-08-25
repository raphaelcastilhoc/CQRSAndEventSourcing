using CQRSAndEventSourcing.Application.SeedWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CQRSAndEventSourcing.Infrastructure.DAO
{
    public interface IEventStore
    {
        Task<IEnumerable<DomainEvent>> GetAsync(Guid aggregateId);

        Task SaveAsync(AggregateRoot agregateRoot);
    }
}
