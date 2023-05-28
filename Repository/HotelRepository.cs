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
        
        
        public async Task<IEnumerable<User>> GetAllCompaniesAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(c => c.HotelName)
                .ToListAsync();
        }
        
        
    }
}