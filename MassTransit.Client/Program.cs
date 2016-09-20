using MassTransit.Client.Services;
using MassTransit.RabbitMqTransport;
using System;

namespace MassTransit.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            RunTransitReceiver();
        }

        private static void RunTransitReceiver()
        {
            string rabbitMqAddress = "rabbitmq://localhost:5672/accounting";
            string rabbitMqQueue = "mycompany.domains.queues";
            Uri rabbitMqRootUri = new Uri(rabbitMqAddress);

            IBusControl control = Bus.Factory.CreateUsingRabbitMq(
                rbt =>
                {
                    IRabbitMqHost host = rbt.Host(
                        rabbitMqRootUri,
                        settings =>
                        {
                            settings.Username("henry");
                            settings.Password("henry");
                        }
                    );

                    // Register the consumer service - 'RegisterCustomerService' with the Service Bus
                    rbt.ReceiveEndpoint(
                        host,
                        rabbitMqQueue,
                        cfgr =>
                        {
                            cfgr.Consumer<RegisterCustomerService>();
                        }
                    );
                }
            );

            control.Start();

            Console.ReadKey();

            control.Stop();
        }
    }
}
