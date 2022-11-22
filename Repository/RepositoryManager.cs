using System.Threading.Tasks;
using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private IHotelRepository _hotelRepository;
        private IAdvertisementRepository _advertisementRepository;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public IHotelRepository Hotel
        {
            get
            {
                if (_hotelRepository == null)
                    _hotelRepository = new HotelRepository(_repositoryContext);
                return _hotelRepository;
            }
        }
        public IAdvertisementRepository Advertisement
        {
            get
            {
                if (_advertisementRepository == null)
                    _advertisementRepository = new AdvertisementRepository(_repositoryContext);
                return _advertisementRepository;
            }
        }
        public void Save() => _repositoryContext.SaveChanges();
        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();

    }
}