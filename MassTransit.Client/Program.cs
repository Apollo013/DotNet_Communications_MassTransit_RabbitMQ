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
            // RunTransitFaultReceiver();
        }

        private static void RunTransitReceiver()
        {
            // IoC - Register Repository
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

                            /******* THE FOLLOWING RETRY POLICIES ARE BASED ON THE OCCURANCE OF EXCEPTIONS ******/
                            //cfgr.UseRetry(Retry.Immediate(5));                                        // Retry 5 times in the event of any exception, before sending to 'error' queue
                            //cfgr.UseRetry(Retry.Except(typeof(ArgumentException)).Immediate(5));      // Retry 5 times Except in the event of an 'ArgumentException'
                            //cfgr.UseRetry(Retry.Selected(typeof(ArgumentException)).Immediate(5));    // Retry 5 times Only in the event of an 'ArgumentException'
                            //cfgr.UseRetry(Retry.All().Immediate(5));                                  // Retry 5 times in the event of any exception
                            /*
                            cfgr.UseRetry(Retry.Filter<Exception>(                                      // Retry 5 times in the event of any exception, and set the error message
                                e => e.Message.IndexOf("Set the error message here") > -1
                            ).Immediate(5));
                            */

                            /******* INTERVAL RETRY POLICIES ******/
                            //cfgr.UseRetry(Retry.Exponential(5, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(5)));
                            //cfgr.UseRetry(Retry.Incremental(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(3)));
                            //cfgr.UseRetry(Retry.Interval(5, TimeSpan.FromSeconds(5)));
                            //cfgr.UseRetry(Retry.Intervals(TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(4)));
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

        private static void RunTransitFaultReceiver()
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



                    // Register the consumer service - 'RegisterCustomerFaultService' with the Service Bus
                    rbt.ReceiveEndpoint(
                        host,
                        ConnectionProperties.FaultEndPoint,
                        cfgr =>
                        {
                            cfgr.Consumer<RegisterCustomerFaultService>();
                            //cfgr.UseRetry(Retry.Interval(5, TimeSpan.FromSeconds(5)));
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
