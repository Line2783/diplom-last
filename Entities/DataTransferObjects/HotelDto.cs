using System;

namespace Entities.DataTransferObjects
{
    public class HotelDto
    {
        public Guid Id { get; set; }
        
        public string INN { get; set; }
        
        public string Email { get; set; }
        
        public string HotelName { get; set; }
    }
}