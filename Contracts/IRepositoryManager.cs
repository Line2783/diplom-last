using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        IClientRepository Client { get; }
        IOrderRepository Order { get; }
        
        IHotelRepository Hotel { get; }
        IAdvertisementRepository Advertisement { get; }
        void Save();
        Task SaveAsync();

    }
}