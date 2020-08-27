using CQRSAndEventSourcing.Application.SeedWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CQRSAndEventSourcing.Infrastructure.DAO
{
    public interface IEventStore
    {
        Task<IEnumerable<DomainEvent>> GetAsync(Guid aggregateId);

        Task<T> GetLastSnapshotAsync<T>(Guid aggregateId) where T : AggregateRoot;

        Task SaveAsync(AggregateRoot agregateRoot, bool isCreationEvent = true);
    }
}
