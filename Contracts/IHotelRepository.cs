using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IHotelRepository
    {
        Task<IEnumerable<Hotel>> GetAllHotelsAsync(bool trackChanges);
        Task<Hotel> GetHotelAsync(Guid hotelId, bool trackChanges);
    }
}