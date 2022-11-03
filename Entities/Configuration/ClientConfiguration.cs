using System;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasData
            (
                new Client
                {
                    Id = new Guid("80515d34-f56c-4e0d-85a9-1e3ebde9abda"),
                    Name = "Sergey Shatrov",
                    Address = "Atemar, house, 125",
                    Age = "20"
                },
                new Client()
                {
                    Id = new Guid("167e2f3a-3c45-452a-b59e-130eaa101d50"),
                    Name = "Sergey Falileev",
                    Address = "Atemar, house, 126",
                    Age = "22"
                }
            );
        }
    }
}