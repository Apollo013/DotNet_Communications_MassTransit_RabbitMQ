using MassTransit.Company.Commands;
using MassTransit.Company.Configuration;
using MassTransit.Publisher.Observers;
using System;
using System.Threading.Tasks;

namespace MassTransit.Publisher
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.Title = "Publisher.";
            Console.WriteLine("CUSTOMER REGISTRATION COMMAND PUBLISHER.");
            RunTransitPublisher();
            //RunTransitFaultPublisher();
        }

        private static void RunTransitPublisher()
        {
            // Create service bus controller
            IBusControl control = Bus.Factory.CreateUsingRabbitMq(
                rbt =>
                {
                    rbt.Host(
                        ConnectionProperties.HostUri,
                        settings =>
                        {
                            settings.Username("henry");
                            settings.Password("henry");
                        }
                    );
                }
            );

            control.ConnectSendObserver(new SendObjectObserver());

            // Create a task that allows us to send the command to a specified queue.
            Task<ISendEndpoint> sendEndpointTask = control.GetSendEndpoint(new Uri($"{ConnectionProperties.HostAddress}/{ConnectionProperties.EndPoint}"));
            ISendEndpoint sendEndPoint = sendEndpointTask.Result;

            // Send command
            Task sendTask = sendEndPoint.Send<IRegisterCustomer>(
                new
                {
                    Address = "New Street",
                    Id = Guid.NewGuid(),
                    Preferred = true,
                    RegisteredDate = DateTime.UtcNow,
                    Name = "A Company Ltd.",
                    Type = 1,
                    DefaultDiscount = 0,
                    Target = "Customers",
                    Importance = 1
                }
            );

            Console.ReadKey();
        }

        private static void RunTransitFaultPublisher()
        {
            // Create service bus controller
            IBusControl control = Bus.Factory.CreateUsingRabbitMq(
                rbt =>
                {
                    rbt.Host(
                        ConnectionProperties.HostUri,
                        settings =>
                        {
                            settings.Username(ConnectionProperties.UserName);
                            settings.Password(ConnectionProperties.Password);
                        }
                    );
                }
            );

            // Create a task that allows us to send the command to a specified queue.
            Task<ISendEndpoint> sendEndpointTask = control.GetSendEndpoint(new Uri($"{ConnectionProperties.HostAddress}/{ConnectionProperties.EndPoint}"));
            ISendEndpoint sendEndPoint = sendEndpointTask.Result;

            // Send command
            Task sendTask = sendEndPoint.Send<IRegisterCustomer>(
                new
                {
                    Address = "New Street",
                    Id = Guid.NewGuid(),
                    Preferred = true,
                    RegisteredDate = DateTime.UtcNow,
                    Name = "Nice people LTD",
                    Type = 1,
                    DefaultDiscount = 0
                },
                // Specify a callback in the event of an error
                callback => callback.FaultAddress = new Uri($"{ConnectionProperties.HostAddress}/{ConnectionProperties.FaultEndPoint}")
            );

            Console.ReadKey();
        }
    }
}
