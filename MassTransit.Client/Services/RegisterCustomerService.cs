using MassTransit.Company.Commands;
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
            IRegisterCustomer customer = context.Message;
            Console.WriteLine($"New Customer for registration: {customer.Name}");
            return Task.FromResult(context.Message);
        }
    }
}
