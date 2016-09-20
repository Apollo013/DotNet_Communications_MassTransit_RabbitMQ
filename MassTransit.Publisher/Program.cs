using MassTransit.Company.Commands;
using System;
using System.Threading.Tasks;

namespace MassTransit.Publisher
{
    class Program
    {
        public static object RegisterNewOrderConsumer { get; private set; }

        static void Main(string[] args)
        {
            RunTransitPublisher();
        }

        private static void RunTransitPublisher()
        {
            string rabbitMqAddress = "rabbitmq://localhost:5672/accounting";
            string rabbitMqQueue = "mycompany.domains.queues";
            Uri rabbitMqRootUri = new Uri(rabbitMqAddress);

            // Create a Service Bus object
            IBusControl control = Bus.Factory.CreateUsingRabbitMq(
                rbt =>
                {
                    rbt.Host(
                        rabbitMqRootUri,
                        settings =>
                        {
                            settings.Username("henry");
                            settings.Password("henry");
                        }
                    );
                }
            );

            Task<ISendEndpoint> sendEndpointTask = control.GetSendEndpoint(new Uri($"{rabbitMqAddress}/{rabbitMqQueue}"));
            ISendEndpoint sendEndPoint = sendEndpointTask.Result;

            Task sendTask = sendEndPoint.Send<IRegisterCustomer>(
                new
                {
                    Address = "New Street",
                    Id = Guid.NewGuid(),
                    Preferred = true,
                    RegisteredDate = DateTime.UtcNow,
                    Name = "A Company Ltd.",
                    Type = 1,
                    DefaultDiscount = 0
                }
            );

            Console.ReadKey();
        }
    }
}
