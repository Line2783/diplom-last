using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Entities.Models;

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

      //   public List<Guid> Image { get; set; }
      // // public List<string> Photos { get; set; }
      //   public static AdvertisementDto Map(Advertisement model)
      //   {
      //       var a= model.Image.Select(p => p.Id).ToList();
      //       return new AdvertisementDto
      //       {
      //           Id = model.Id,
      //           Name = model.Name,
      //           City = model.City,
      //           Address = model.Address,
      //           Description = model.Description,
      //           Number = model.Number,
      //           Cat = model.Cat,
      //           Dog = model.Dog,
      //           Rodent = model.Rodent,
      //           Other = model.Other,
      //           Image = a,
      //   
      //   
      //       };
      //   
      //   }
    }
}
