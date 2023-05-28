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
    public class HotelRepository: RepositoryBase<User>, IHotelRepository
    {
        public HotelRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        // public async Task<IEnumerable<Companyy>> GetAllHotelsAsync(bool trackChanges)
        //     => await FindAll(trackChanges)
        //         .OrderBy(c => c.HotelName)
        //         .ToListAsync();
        
        // public async Task<IEnumerable<User>> GetAllHotelsAsync(bool trackChanges)
        // {
        //     return await FindAll(trackChanges).ToListAsync();
        // }
        //
        // public async Task<User> GetHotelAsync(Guid hotelId, bool trackChanges) =>
        //     await FindByCondition(c => c.Id.Equals(hotelId), trackChanges)
        //         .SingleOrDefaultAsync();
        //
        // public async Task<IEnumerable<User>> GetByIdsAsync(IEnumerable<Guid> ids, bool
        //     trackChanges) =>
        //     await FindByCondition(x => ids.Contains(x.Id), trackChanges)
        //         .ToListAsync();
        
        public async Task<IEnumerable<User>> GetAllCompaniesAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(c => c.HotelName)
                .ToListAsync();
        }
        
        
    }
}