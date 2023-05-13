using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IProductPhotoService 
    {
        Task<IEnumerable<ProductPhoto>> GetAllProductPhotoByIdAsync(Guid photoId);
        Task AddPhoto(ProductPhoto productPhoto);
        Task<ProductPhoto?> GetFileAsync(Guid imageId);
    }
}