using System.Linq;
using System.Linq.Dynamic.Core;
using Entities.Models;
using Repository.Extensions.Utility;

namespace Repository.Extensions
{
    public static class RepositoryAdvertisementExtensions
    {
        
        public static IQueryable<Advertisement> Search(this IQueryable<Advertisement>
                advertisement,
            string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return advertisement;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return advertisement.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
            
        }
        public static IQueryable<Advertisement> Sort(this IQueryable<Advertisement> advertisement,
            string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return advertisement.OrderBy(e => e.Name);

            var orderQuery =
                OrderQueryBuilder.CreateOrderQuery<Advertisement>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return advertisement.OrderBy(e => e.Name);
            return advertisement.OrderBy(orderQuery);
        }
    }
}