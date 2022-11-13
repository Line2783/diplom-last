using System.Linq;
using System.Linq.Dynamic.Core;
using Entities.Models;
using Repository.Extensions.Utility;

namespace Repository.Extensions
{
    public static class RepositoryOrderExtensions
    {
        public static IQueryable<Order> FilterOrders(this IQueryable<Order>
            orders, uint MinQuantity, uint MaxQuantity) =>
            orders.Where(e => (e.Quantity >= MinQuantity && e.Quantity <= MaxQuantity));
        public static IQueryable<Order> Search(this IQueryable<Order>
                orders,
            string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return orders;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return orders.Where(e => e.Product.ToLower().Contains(lowerCaseTerm));
            
        }
        public static IQueryable<Order> Sort(this IQueryable<Order> orders,
            string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return orders.OrderBy(e => e.Product);

            var orderQuery =
                OrderQueryBuilder.CreateOrderQuery<Order>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return orders.OrderBy(e => e.Product);
            return orders.OrderBy(orderQuery);
        }
    }
}