using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;
using Entities.RequestFeatures;
using Entities.RequestFeaturess;

namespace Contracts
{
    public interface IAdvertisementRepository
    {
        Task<IEnumerable<Advertisement>> GetAllAdvertisementsAsync(bool trackChanges);
        Task<Advertisement> GetAdvertisementAsync(Guid advertisementId, bool trackChanges);
        // void CreateAdvertisementForHotel(Guid hotelId, Advertisement advertisement);
        void CreateAdvertisement(Advertisement advertisement);
        void DeleteAdvertisement(Advertisement advertisement);
        
        Task<IEnumerable<Advertisement>> GetAdvertisementsAsync(Guid hotelId,  bool trackChanges);


        Task<IEnumerable<Advertisement>> GetAdvertisementsForCompanyAsync(string companyId, bool trackChanges);    
    }
}