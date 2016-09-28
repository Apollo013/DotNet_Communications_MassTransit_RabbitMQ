using MassTransit.Company.Commands;
using System;
using System.Threading.Tasks;

namespace MassTransit.Client.Services
{
    public class RegisterCustomerFaultService : IConsumer<Fault<IRegisterCustomer>>
    {
        public Task Consume(ConsumeContext<Fault<IRegisterCustomer>> context)
        {
            Console.WriteLine($"FAULT HANDLED");
            IRegisterCustomer originalFault = context.Message.Message;
            ExceptionInfo[] exceptions = context.Message.Exceptions;
            return Task.FromResult(originalFault);
        }
    }
}
