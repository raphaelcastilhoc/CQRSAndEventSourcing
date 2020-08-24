using MediatR;

namespace CQRSAndEventSourcing.Application.Commands
{
    public class AddEmployeeCommand : IRequest
    {
        public string Name { get; set; }

        public decimal Salary { get; set; }
    }
}
