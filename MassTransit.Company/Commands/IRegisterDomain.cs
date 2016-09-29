namespace MassTransit.Company.Commands
{
    public interface IRegisterDomain
    {
        string Target { get; }
        int Importance { get; }
    }
}
