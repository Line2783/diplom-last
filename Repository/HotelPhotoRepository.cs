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
    public class HotelPhotoRepository : RepositoryBase<HotelPhoto>, IHotelPhotoRepository
    {
        public HotelPhotoRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task SaveFilesAsync(HotelPhoto hotelPhoto) => Create(hotelPhoto);

        public async Task<HotelPhoto?> GetFileAsync(Guid photoId, bool trackChanges) =>
            await FindByCondition(p => p.Id.Equals(photoId), trackChanges)
                .SingleOrDefaultAsync();

        public async Task<ICollection<HotelPhoto>> GetAllProductPhotoAsync(Guid hotelId, bool trackChanges) =>
            await FindAll(trackChanges).OrderBy(o => o.HotelId == hotelId)
                .Where(x => x.HotelId == hotelId)
                .ToListAsync();

        public async Task SaveRepositoryAsync() => await SaveAsync();
    }
}