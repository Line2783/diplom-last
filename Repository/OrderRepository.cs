using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public IEnumerable<Order> GetOrders(Guid orderId, bool trackChanges) => 
            FindByCondition(e => e.ClientId.Equals(orderId), trackChanges).OrderBy(e => e.Product);
    }
}