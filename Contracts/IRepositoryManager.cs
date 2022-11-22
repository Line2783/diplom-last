using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IHotelRepository Hotel { get; }
        IAdvertisementRepository Advertisement { get; }
        void Save();
        Task SaveAsync();

    }
}