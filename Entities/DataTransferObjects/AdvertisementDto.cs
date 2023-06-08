using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Entities.Models;

namespace Entities.DataTransferObjects
{
    public class AdvertisementDto
    {
        public Guid AdvertisementId { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string Address { get; set; }
        
        public string Description { get; set; }

        public string Number { get; set; }

        public Boolean Cat { get; set; }

        public Boolean Dog { get; set; }

        public Boolean Rodent { get; set; }

        public Boolean Other { get; set; }
        
        public string? CompanyId { get; set; } 
        
        public List<string> Photos { get; set; }
        
       
    }
}
