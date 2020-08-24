using CQRSAndEventSourcing.Application.SeedWork;
using System;

namespace CQRSAndEventSourcing.Application.Events
{
    public class EmployeeAddedDomainEvent : DomainEvent
    {
        public EmployeeAddedDomainEvent(Guid id,
            string name,
            decimal salary)
        {
            Id = id;
            Name = name;
            Salary = salary;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public decimal Salary { get; private set; }
    }
}
