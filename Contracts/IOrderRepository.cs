using System;
using System.Collections.Generic;
using Entities.Models;

namespace Contracts
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrders(Guid orderId, bool trackChanges);
        Order GetOrder(Guid orderId, Guid id, bool trackChanges);
        void CreateOrderForClient(Guid clientId, Order order);
        void DeleteOrder(Order order);



    }
}