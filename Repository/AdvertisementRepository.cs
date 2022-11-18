using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class AdvertisementRepository : RepositoryBase<Advertisement>, IAdvertisementRepository
    {
        public AdvertisementRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}