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
        
        public async Task<IEnumerable<Advertisement>> GetAdvertisementsAsync(Guid advertisementId,
            bool trackChanges)=>
            await FindByCondition(e => e.AdvertisementId.Equals(advertisementId), trackChanges)
                .OrderBy(e => e.Name).ToListAsync();
        
        public async Task<IEnumerable<Advertisement>> GetAllAdvertisementsAsync(bool trackChanges)
            => await FindAll(trackChanges)
                .OrderBy(c => c.Name)
                .ToListAsync();

        public async Task<Advertisement> GetAdvertisementAsync(Guid advertisementId,  bool trackChanges) =>
            await FindByCondition(c => c.AdvertisementId.Equals(advertisementId), trackChanges)
                .SingleOrDefaultAsync();
        
        public void CreateAdvertisement(Advertisement advertisement) => Create(advertisement);
        
        public void DeleteAdvertisement(Advertisement advertisement)
        {
            Delete(advertisement);
        }
        
        public async Task<IEnumerable<Advertisement>> GetAdvertisementsForCompanyAsync(string companyId, bool trackChanges) =>
            await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
                .OrderBy(e => e.Name).ToListAsync();
        
        
    }
}