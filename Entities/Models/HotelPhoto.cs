using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class HotelPhoto
    {
        [Column("PhotoId")]
        public Guid Id { get; set; }
        [ForeignKey(nameof(Hotel))]
        public Guid HotelId { get; set; }
        public Hotel Hotel { get; set; }
        public byte[] Photo { get; set; }
        public string Name { get; set; }
    }
}