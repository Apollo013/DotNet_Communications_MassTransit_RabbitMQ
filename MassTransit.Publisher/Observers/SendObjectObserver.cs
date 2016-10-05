using System;
using System.Threading.Tasks;

namespace MassTransit.Publisher.Observers
{
    public class SendObjectObserver : ISendObserver
    {
        private readonly string _logger = "***[SendObjectObserver]***";
        public async Task PostSend<T>(SendContext<T> context) where T : class
        {
            Console.WriteLine();
            Console.WriteLine($"{_logger}: A message is past the send stage for type: { context.Message.GetType()}");
            await Task.FromResult(0);
        }

        public async Task PreSend<T>(SendContext<T> context) where T : class
        {
            Console.WriteLine();
            Console.WriteLine($"{_logger}: A message is before the send stage for type: { context.Message.GetType()}");
            await Task.FromResult(0);
        }

        public async Task SendFault<T>(SendContext<T> context, Exception exception) where T : class
        {
            Console.WriteLine();
            Console.WriteLine($"{_logger}: A message fault is sent for the type {context.Message.GetType()} and has been received with exception {exception.Message}");
            await Task.FromResult(0);
        }
    }
}
