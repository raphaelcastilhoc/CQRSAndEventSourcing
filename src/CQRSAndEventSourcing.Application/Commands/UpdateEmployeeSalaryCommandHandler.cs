using CQRSAndEventSourcing.Application.Aggregates.EmployeeAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSAndEventSourcing.Application.Commands
{
    public class UpdateEmployeeSalaryCommandHandler : IRequestHandler<UpdateEmployeeSalaryCommand>
    {
        private IEmployeeRepository _employeeRepository;

        public UpdateEmployeeSalaryCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Unit> Handle(UpdateEmployeeSalaryCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetAsync(request.Id);
            employee.UpdateSalary(request.Salary);

            await _employeeRepository.UpdateAsync(employee);

            return Unit.Value;
        }
    }
}
