using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Entities.RequestFeaturess;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;

namespace Repository
{
    public class AdvertisementRepository : RepositoryBase<Advertisement>, IAdvertisementRepository
    {
        public AdvertisementRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        
        

        public async Task<IEnumerable<Advertisement>> GetAllAdvertisementsAsync(bool trackChanges)
            => await FindAll(trackChanges)
                .OrderBy(c => c.Name)
                .ToListAsync();

        public async Task<Advertisement> GetAdvertisementAsync(Guid advertisementId, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(advertisementId), trackChanges)
                .SingleOrDefaultAsync();
    }
}