using System;
using System.Collections.Generic;
using Entities.Models;

namespace Contracts
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrders(Guid orderId, bool trackChanges);
    }
}