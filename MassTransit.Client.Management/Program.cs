using MassTransit.Client.Management.EventHandlers;
using MassTransit.Company.Configuration;
using MassTransit.RabbitMqTransport;
using System;

namespace MassTransit.Client.Management
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Management Receiver.";
            Console.WriteLine("MANAGEMENT CUSTOMER REGISTRATION EVENT RECEIVER.");
            RunTransitEventReceiver();
        }

        private static void RunTransitEventReceiver()
        {
            // Create service bus controller
            // This will listen for commands on the queue called 'mycompany.domains.queues'
            IBusControl control = Bus.Factory.CreateUsingRabbitMq(
                rbt =>
                {
                    IRabbitMqHost host = rbt.Host(
                        ConnectionProperties.HostUri,
                        settings =>
                        {
                            settings.Username(ConnectionProperties.UserName);
                            settings.Password(ConnectionProperties.Password);
                        }
                    );

                    // Register the consumer service - 'CustomerRegisteredHandler' with the Service Bus
                    rbt.ReceiveEndpoint(
                        host,
                        ConnectionProperties.EndPoint,
                        cfgr =>
                        {
                            cfgr.Consumer<CustomerRegisteredHandler>();
                        }
                    );
                }
            );

            // Start listening
            control.Start();

            // Wait for triggered events
            Console.ReadKey();

            // Stop Listening
            control.Stop();
        }
    }
}
