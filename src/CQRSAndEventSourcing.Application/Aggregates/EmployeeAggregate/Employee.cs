using CQRSAndEventSourcing.Application.Events;
using CQRSAndEventSourcing.Application.SeedWork;
using System;

namespace CQRSAndEventSourcing.Application.Aggregates.EmployeeAggregate
{
    public class Employee : AggregateRoot
    {
        public Employee(string name,
            decimal salary) : base(Guid.NewGuid())
        {
            Name = name;
            Salary = salary;

            AddDomainEvent(new EmployeeAddedDomainEvent(Id, name, salary));
        }

        public string Name { get; private set; }

        public decimal Salary { get; private set; }

        public void UpdateSalary(decimal salary)
        {
            Salary = salary;
        }
    }
}
