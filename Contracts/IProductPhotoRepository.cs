using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IProductPhotoRepository
    {
        
            Task SaveFilesAsync(ProductPhoto productPhoto);
            Task<ProductPhoto?> GetFileAsync(Guid imageId, bool trackChanges);
            Task<ICollection<ProductPhoto>> GetAllProductPhotoAsync(Guid productId, bool trackChanges);
            Task SaveRepositoryAsync();
    }
}