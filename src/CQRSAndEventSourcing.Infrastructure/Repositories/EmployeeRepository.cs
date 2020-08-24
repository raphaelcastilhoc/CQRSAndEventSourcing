using CQRSAndEventSourcing.Application.Aggregates.EmployeeAggregate;
using CQRSAndEventSourcing.Infrastructure.DAO;
using System;
using System.Threading.Tasks;

namespace CQRSAndEventSourcing.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IEventStore _eventStore;

        public EmployeeRepository(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<Employee> GetAsync(Guid id)
        {
            //await _eventStore.SaveAsync(employee);

            return new Employee("test", 1);
        }

        public async Task SaveAsync(Employee employee)
        {
            await _eventStore.SaveAsync(employee);
        }
    }
}
