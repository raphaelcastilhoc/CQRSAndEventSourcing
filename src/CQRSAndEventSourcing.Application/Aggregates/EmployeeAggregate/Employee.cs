using CQRSAndEventSourcing.Application.Events;
using CQRSAndEventSourcing.Application.SeedWork;
using System;
using System.Collections.Generic;

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

        public Employee(IEnumerable<DomainEvent> domainEvents) : base(domainEvents)
        {
        }

        public string Name { get; private set; }

        public decimal Salary { get; private set; }

        public void UpdateSalary(decimal salary)
        {
            Salary = salary;
            AddDomainEvent(new EmployeeSalaryUpdatedDomainEvent(salary));
        }

        public void On(EmployeeAddedDomainEvent @event)
        {
            Id = @event.Id;
            Name = @event.Name;
            Salary = @event.Salary;
        }

        public void On(EmployeeSalaryUpdatedDomainEvent @event)
        {
            Salary = @event.Salary;
        }
    }
}
