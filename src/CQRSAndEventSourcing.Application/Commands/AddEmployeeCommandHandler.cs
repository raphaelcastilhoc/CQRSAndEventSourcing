using CQRSAndEventSourcing.Application.Aggregates.EmployeeAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSAndEventSourcing.Application.Commands
{
    public class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand>
    {
        private IEmployeeRepository _employeeRepository;

        public AddEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Unit> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee(request.Name, request.Salary);
            await _employeeRepository.AddAsync(employee);

            return Unit.Value;
        }
    }
}
