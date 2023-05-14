using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IHotelPhotoRepository
    {
        Task SaveFilesAsync(HotelPhoto hotelPhoto);
        Task<HotelPhoto?> GetFileAsync(Guid imageId, bool trackChanges);
        Task<ICollection<HotelPhoto>> GetAllProductPhotoAsync(Guid productId, bool trackChanges);
        Task SaveRepositoryAsync();
    }
}