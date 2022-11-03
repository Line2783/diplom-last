namespace Contracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        IClientRepository Client { get; }
        IOrderRepository Order { get; }
        void Save();
    }
}