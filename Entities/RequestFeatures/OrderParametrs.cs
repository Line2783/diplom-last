namespace Entities.RequestFeatures
{
    public abstract class OrderParametrs
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > maxPageSize) ? maxPageSize : value; }
        }
    }
    public class OrderParameters : RequestParameters
    {
        public uint MinQuantity { get; set; }
        public uint MaxQuantity { get; set; } = int.MaxValue;
        public bool ValidAgeRange => MaxQuantity > MinQuantity;
    }
}