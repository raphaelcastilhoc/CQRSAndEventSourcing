using CQRSAndEventSourcing.Application.SeedWork;

namespace CQRSAndEventSourcing.Application.Events
{
    public class EmployeeSalaryUpdatedDomainEvent : DomainEvent
    {
        public EmployeeSalaryUpdatedDomainEvent(decimal salary)
        {
            Salary = salary;
        }

        public decimal Salary { get; private set; }
    }
}
