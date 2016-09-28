using MassTransit.Company.Commands;
using MassTransit.Company.Events;
using MassTransit.Company.Models;
using MassTransit.Company.Repositories;
using System;
using System.Threading.Tasks;

namespace MassTransit.Client.Services
{
    /// <summary>
    /// Service for registering customers
    /// </summary>
    public class RegisterCustomerService : IConsumer<IRegisterCustomer>
    {
        private readonly ICustomerRepository _customerRepository;

        public RegisterCustomerService(ICustomerRepository customerRepository)
        {
            if (customerRepository == null)
            {
                throw new ArgumentNullException("Customer repository");
            }
            _customerRepository = customerRepository;
        }

        public Task Consume(ConsumeContext<IRegisterCustomer> context)
        {
            // Execute the command (here we simply output the message)
            IRegisterCustomer customer = context.Message;
            Console.WriteLine($"New Customer for registration: {customer.Name}");

            throw new ArgumentException("We'll pretend that an exception was thrown...");

            // Save customer to dummy db
            _customerRepository.Save(new Customer(customer.Id, customer.Name, customer.Address)
            {
                DefaultDiscount = customer.DefaultDiscount,
                Preferred = customer.Preferred,
                RegisteredDate = customer.RegisteredDate,
                Type = customer.Type
            });

            // Raise an event in response to the command being executed
            context.Publish<ICustomerRegistered>(
                new
                {
                    Address = customer.Address,
                    Id = customer.Id,
                    RegisteredDate = customer.RegisteredDate,
                    Name = customer.Name
                }
            );

            return Task.FromResult(context.Message);
        }
    }
}
