using MassTransit.Client.Services;
using MassTransit.Company.Configuration;
using MassTransit.Company.Repositories;
using MassTransit.RabbitMqTransport;
using StructureMap;
using System;

namespace MassTransit.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Receiver.";
            Console.WriteLine("CUSTOMER REGISTRATION COMMAND RECEIVER.");
            RunTransitReceiver();
        }

        private static void RunTransitReceiver()
        {
            // IoC
            var container = new Container(conf =>
            {
                conf.For<ICustomerRepository>().Use<CustomerRepository>();
            });

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

                    // Register the consumer service - 'RegisterCustomerService' with the Service Bus
                    rbt.ReceiveEndpoint(
                        host,
                        ConnectionProperties.EndPoint,
                        cfgr =>
                        {
                            cfgr.Consumer<RegisterCustomerService>(container);
                        }
                    );
                }
            );

            // Start listening
            control.Start();

            // Wait for commands
            Console.ReadKey();

            // Stop Listening
            control.Stop();
        }
    }
}
