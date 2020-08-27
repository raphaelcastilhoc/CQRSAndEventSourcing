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
            var lastSnapshot = await _eventStore.GetLastSnapshotAsync(id);
            if(lastSnapshot != null)
            {
                var employee = lastSnapshot as Employee;
                var domainEvents = await _eventStore.GetAsync(id, employee.Version);
                employee.ApplyDomainEvents(domainEvents);

                return employee;
            }
            else
            {
                var domainEvents = await _eventStore.GetAsync(id);
                return new Employee(domainEvents);
            }
        }

        public async Task AddAsync(Employee employee)
        {
            await _eventStore.SaveAsync(employee);
        }

        public async Task UpdateAsync(Employee employee)
        {
            await _eventStore.SaveAsync(employee, false);
        }
    }
}
