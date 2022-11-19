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
    }
}