using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;

namespace diplom.Service
{
    public class HotelPhotoService : IHotelPhotoService
    {
        private readonly IHotelPhotoRepository _repository;

        public HotelPhotoService(IHotelPhotoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<HotelPhoto>> GetAllProductPhotoByIdAsync(Guid photoId)
        {
            return await _repository.GetAllProductPhotoAsync(photoId, false);
        }
        
        public async Task AddPhoto(HotelPhoto hotelPhoto)
        {
            await _repository.SaveFilesAsync(hotelPhoto);
            await _repository.SaveRepositoryAsync();
        }
        
        public async Task<HotelPhoto?> GetFileAsync(Guid imageId)
        {
            return await _repository.GetFileAsync(imageId, trackChanges: false);
        }
    }
}