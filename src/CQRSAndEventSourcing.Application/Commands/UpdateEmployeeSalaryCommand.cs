using MediatR;
using System;

namespace CQRSAndEventSourcing.Application.Commands
{
    public class UpdateEmployeeSalaryCommand : IRequest
    {
        public Guid Id { get; set; }

        public decimal Salary { get; set; }
    }
}
