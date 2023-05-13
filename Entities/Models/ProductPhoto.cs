using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class ProductPhoto
    {
        [Column("PhotoId")]
        public Guid Id { get; set; }
        [ForeignKey(nameof(Advertisement))]
        public Guid AdvertisementId { get; set; }
        public Advertisement Advertisement { get; set; }
        public byte[] Photo { get; set; }
        public string Name { get; set; }
    }
}