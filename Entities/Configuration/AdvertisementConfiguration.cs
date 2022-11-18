using System;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
    public class AdvertisementConfiguration : IEntityTypeConfiguration<Advertisement>
    {
        public void Configure(EntityTypeBuilder<Advertisement> builder)
        {
            builder.HasData
            (
                new Advertisement
                {
                    Id = new Guid("933e8244-d563-476c-b820-5c6587b6235a"),
                    Name = "LoveYourPats",
                    City = "Saransk",
                    Address = "Bolshevistskaya street,68",
                    Description = "Loves your pats very much",
                    Number = "89271478520",
                    Cat = true,
                    Dog = true,
                    Rodent = true,
                    Other = true
                    
                    
                },
                new Advertisement
                {
                    Id = new Guid("933e8244-d563-476c-b820-5c6587b6235a"),
                    Name = "LoveYourCats",
                    City = "Saransk",
                    Address = "Bolshevistskaya street,69",
                    Description = "Loves your cats so very much:)",
                    Number = "89271478520",
                    Cat = true,
                    Dog = false,
                    Rodent = false,
                    Other = false
                }
            );
        }  
    }
}