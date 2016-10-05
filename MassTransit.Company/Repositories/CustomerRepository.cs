using MassTransit.Company.Models;
using System;

namespace MassTransit.Company.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public void Save(Customer customer)
        {
            Console.WriteLine();
            Console.WriteLine($"[CustomerRepository]: The concrete customer repository was called for customer {customer.Name}");
        }
    }
}
