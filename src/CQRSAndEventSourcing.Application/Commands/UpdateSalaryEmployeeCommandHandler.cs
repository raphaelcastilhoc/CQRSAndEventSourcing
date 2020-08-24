using CQRSAndEventSourcing.Application.Aggregates.EmployeeAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSAndEventSourcing.Application.Commands
{
    public class UpdateSalaryEmployeeCommandHandler : IRequestHandler<UpdateSalaryEmployeeCommand>
    {
        private IEmployeeRepository _employeeRepository;

        public UpdateSalaryEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Unit> Handle(UpdateSalaryEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetAsync(request.Id);
            employee.UpdateSalary(request.Salary);

            await _employeeRepository.SaveAsync(employee);

            return Unit.Value;
        }
    }
}
