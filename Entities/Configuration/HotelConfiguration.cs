using System;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData
            (
                new Hotel
                {
                    Id = new Guid("099153b0-3e4d-46f4-893e-e90c5589bc84"),
                    INN = "147258963101",
                    Email = "hellopet@mail.ru",
                    HotelName = "LoveYourPets"
                },
                new Hotel
                {
                    Id = new Guid("052678b7-7705-4910-ac55-30e7a224b76d"),
                    INN = "512369874121",
                    Email = "getpet@gmail.com",
                    HotelName = "WishesPet"
                }
            );
        }
    }
}