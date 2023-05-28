using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IHotelRepository
    {
        Task<IEnumerable<User>> GetAllCompaniesAsync(bool trackChanges);
        
        
    }
}