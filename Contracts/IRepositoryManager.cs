using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IHotelRepository Hotel { get; }
        IAdvertisementRepository Advertisement { get; }
        IProductPhotoRepository ProductPhoto { get; }
        IHotelPhotoRepository HotelPhoto { get; }
        void Save();
        Task SaveAsync();

    }
}