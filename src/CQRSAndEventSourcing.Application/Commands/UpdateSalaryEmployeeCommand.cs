using MediatR;
using System;

namespace CQRSAndEventSourcing.Application.Commands
{
    public class UpdateSalaryEmployeeCommand : IRequest
    {
        public Guid Id { get; set; }

        public decimal Salary { get; set; }
    }
}
