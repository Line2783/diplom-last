using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IHotelRepository
    {
        // Task<IEnumerable<Companyy>> GetAllHotelsAsync(bool trackChanges);
        Task<IEnumerable<User>> GetAllCompaniesAsync(bool trackChanges);
        // Task<Companyy> GetHotelAsync(Guid hotelId, bool trackChanges);
        // Task<IEnumerable<Companyy>> GetByIdsAsync(IEnumerable<Guid> ids, bool
        //     trackChanges);
        
    }
}