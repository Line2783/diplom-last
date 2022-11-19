using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class HotelRepository: RepositoryBase<Hotel>, IHotelRepository
    {
        public HotelRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public async Task<IEnumerable<Hotel>> GetAllHotelsAsync(bool trackChanges)
            => await FindAll(trackChanges)
                .OrderBy(c => c.HotelName)
                .ToListAsync();
        
        public async Task<Hotel> GetHotelAsync(Guid hotelId, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(hotelId), trackChanges)
                .SingleOrDefaultAsync();
    }
}