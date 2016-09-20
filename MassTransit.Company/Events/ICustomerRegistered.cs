using System;

namespace MassTransit.Company.Events
{
    /// <summary>
    /// Event message contract for a newly registered customer
    /// </summary>
    public interface ICustomerRegistered
    {
        Guid Id { get; }
        DateTime RegisteredDate { get; }
        string Name { get; }
        string Address { get; }
    }
}
