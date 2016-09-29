using MassTransit.Company.Commands;
using System;
using System.Threading.Tasks;

namespace MassTransit.Client.Services
{
    public class RegisterDomainService : IConsumer<IRegisterDomain>
    {
        public Task Consume(ConsumeContext<IRegisterDomain> context)
        {
            Console.WriteLine($"New domain registered. Target and importance: {context.Message.Target} - {context.Message.Importance}");
            return Task.FromResult<IRegisterDomain>(context.Message);
        }
    }
}
