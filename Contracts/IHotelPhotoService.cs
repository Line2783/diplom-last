using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IHotelPhotoService
    {
        Task<IEnumerable<HotelPhoto>> GetAllProductPhotoByIdAsync(Guid photoId);
        Task AddPhoto(HotelPhoto hotelPhoto);
        Task<HotelPhoto?> GetFileAsync(Guid imageId);
    }
}