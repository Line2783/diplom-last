using System;

namespace Entities.DataTransferObjects
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public int Cost { get; set; }
    }
}