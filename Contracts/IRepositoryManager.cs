using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        
        IAdvertisementRepository Advertisement { get; }
        IProductPhotoRepository ProductPhoto { get; }
        IHotelRepository User { get; }

        void Save();
        Task SaveAsync();

    }
}