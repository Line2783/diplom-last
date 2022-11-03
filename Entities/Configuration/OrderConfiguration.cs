using System;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasData
            (
                new Order
                {
                    Id = new Guid("b9fc089c-463a-47d5-9763-1bd0a94f1f03"),
                    Product = "Oil Motul",
                    Quantity = 3,
                    Cost = 3000
                },
                new Order
                {
                    Id = new Guid("92a60611-bff8-487f-ab72-7c27a073fd19"),
                    Product = "Oil Ralf",
                    Quantity = 1,
                    Cost = 1500
                }
            );
        }
    }
}