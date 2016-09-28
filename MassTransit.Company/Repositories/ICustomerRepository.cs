using MassTransit.Company.Models;

namespace MassTransit.Company.Repositories
{
    public interface ICustomerRepository
    {
        void Save(Customer customer);
    }
}
