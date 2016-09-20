using MassTransit.Company.Commands;
using MassTransit.Company.Events;
using System;
using System.Threading.Tasks;

namespace MassTransit.Client.Services
{
    /// <summary>
    /// Service for registering customers
    /// </summary>
    public class RegisterCustomerService : IConsumer<IRegisterCustomer>
    {
        public Task Consume(ConsumeContext<IRegisterCustomer> context)
        {
            // Execute the command (here we simply output the message)
            IRegisterCustomer customer = context.Message;
            Console.WriteLine($"New Customer for registration: {customer.Name}");

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
