using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class HotelRepository: RepositoryBase<Hotel>, IHotelRepository
    {
        public HotelRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}