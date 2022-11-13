using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;

namespace Repository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository 
    {
        public OrderRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        
         public async Task<PagedList<Order>> GetOrdersAsync (Guid clientId, OrderParameters orderParameters, 
             bool trackChanges)
        {
           
            var orders = await FindByCondition(e => e.ClientId.Equals(clientId),
                    trackChanges)
                .FilterOrders(orderParameters.MinQuantity, orderParameters.MaxQuantity)
                .Search(orderParameters.SearchTerm)
                .Sort(orderParameters.OrderBy)
                .ToListAsync();
            return PagedList<Order>
                .ToPagedList(orders, orderParameters.PageNumber,
                    orderParameters.PageSize);
        }
        
        public async Task<Order> GetOrderAsync(Guid clientId, Guid id, bool trackChanges) =>
            await FindByCondition(e => e.ClientId.Equals(clientId) && e.Id.Equals(id),
                trackChanges).SingleOrDefaultAsync();
        
        public  void CreateOrderForClient(Guid clientId, Order order)
        {
            order.ClientId = clientId;
            Create(order);
        }
        
        public void DeleteOrder(Order order)
        {
            Delete(order);
        }
    }
}