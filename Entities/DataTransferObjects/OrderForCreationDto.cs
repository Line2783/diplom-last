namespace Entities.DataTransferObjects
{
    public class OrderForCreationDto
    {
        public string Product { get; set; }
        public int Quantity { get; set; }
        public int Cost { get; set; }
    }
}