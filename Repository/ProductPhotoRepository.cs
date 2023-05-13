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
    public class ProductPhotoRepository : RepositoryBase<ProductPhoto>, IProductPhotoRepository
    {
        public ProductPhotoRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task SaveFilesAsync(ProductPhoto productPhoto) => Create(productPhoto);

        public async Task<ProductPhoto?> GetFileAsync(Guid photoId, bool trackChanges) =>
            await FindByCondition(p => p.Id.Equals(photoId), trackChanges)
                .SingleOrDefaultAsync();

        public async Task<ICollection<ProductPhoto>> GetAllProductPhotoAsync(Guid advertisementId, bool trackChanges) =>
            await FindAll(trackChanges).OrderBy(o => o.AdvertisementId == advertisementId)
                .Where(x => x.AdvertisementId == advertisementId)
                .ToListAsync();

        public async Task SaveRepositoryAsync() => await SaveAsync();
    }
}