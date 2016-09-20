using MassTransit.Company.Events;
using System;
using System.Threading.Tasks;

namespace MassTransit.Client.Management.EventHandlers
{
    public class CustomerRegisteredHandler : IConsumer<ICustomerRegistered>
    {
        public Task Consume(ConsumeContext<ICustomerRegistered> context)
        {
            ICustomerRegistered newCustomer = context.Message;
            Console.WriteLine("Management: A new customer has been registered");
            Console.WriteLine(newCustomer.Address);
            Console.WriteLine(newCustomer.Name);
            Console.WriteLine(newCustomer.Id);
            return Task.FromResult(context.Message);
        }
    }
}
