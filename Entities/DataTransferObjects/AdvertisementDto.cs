using System;

namespace Entities.DataTransferObjects
{
    public class AdvertisementDto
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string City { get; set; }
        
        public string Address { get; set; }
        
        public string Description { get; set; }
        
        public string Number { get; set; }
        
        public Boolean Cat { get; set; }
        
        public Boolean Dog { get; set; }
        
        public Boolean Rodent { get; set; }
        
        public Boolean Other { get; set; }
        
    }
}