using System;

namespace MassTransit.Company.Commands
{
    /// <summary>
    /// Command message contract for registering a new customer
    /// </summary>
    public interface IRegisterCustomer
    {
        Guid Id { get; }
        DateTime RegisteredDate { get; }
        int Type { get; }
        string Name { get; }
        bool Preferred { get; }
        decimal DefaultDiscount { get; }
        string Address { get; }
    }
}
