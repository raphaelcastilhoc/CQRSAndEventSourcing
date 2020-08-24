using CQRSAndEventSourcing.Application.SeedWork;
using System.Threading.Tasks;

namespace CQRSAndEventSourcing.Infrastructure.DAO
{
    public interface IEventStore
    {
        Task SaveAsync(AggregateRoot agregateRoot);
    }
}
