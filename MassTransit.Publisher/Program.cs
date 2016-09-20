using MassTransit.Company.Commands;
using MassTransit.Company.Configuration;
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
                    DefaultDiscount = 0
                }
            );

            Console.ReadKey();
        }
    }
}
