using System;
using System.Threading.Tasks;

namespace CQRSAndEventSourcing.Application.Aggregates.EmployeeAggregate
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetAsync(Guid id);

        Task AddAsync(Employee employee);

        Task UpdateAsync(Employee employee);
    }
}
