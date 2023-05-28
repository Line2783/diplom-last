using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;

namespace diplom.Service
{
    public class ProductPhotoService : IProductPhotoService
    {
        private readonly IProductPhotoRepository _repository;

        public ProductPhotoService(IProductPhotoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductPhoto>> GetAllProductPhotoByIdAsync(Guid photoId)
        {
            return await _repository.GetAllProductPhotoAsync(photoId, false);
        }
        
        public async Task AddPhoto(ProductPhoto productPhoto)
        {
            await _repository.SaveFilesAsync(productPhoto);
            await _repository.SaveRepositoryAsync();
        }
        
        public async Task<ProductPhoto?> GetFileAsync(Guid advertisementId)
        {
            return await _repository.GetFileAsync(advertisementId, trackChanges: false);
        }
    
    }
}