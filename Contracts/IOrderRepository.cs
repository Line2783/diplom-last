using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;
using Entities.RequestFeatures;
using Entities.RequestFeaturess;

namespace Contracts
{
    public interface IOrderRepository
    {
        Task<PagedList<Order>> GetOrdersAsync(Guid clientId, OrderParameters
            orderParameters, bool trackChanges);
        Task<Order> GetOrderAsync(Guid clientId, Guid id, bool trackChanges);
        void CreateOrderForClient(Guid clientId, Order order);
        void DeleteOrder(Order order);



    }
}