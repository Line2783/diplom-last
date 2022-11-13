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
        public string OrderBy { get; set; }
        public string Fields { get; set; }

        
    }
    public class OrderParameters : RequestParameters
    {
        public OrderParameters()
        {
            OrderBy = "name";
        }
        public uint MinQuantity { get; set; }
        public uint MaxQuantity { get; set; } = int.MaxValue;
        public bool ValidAgeRange => MaxQuantity > MinQuantity;
        public string SearchTerm { get; set; }

    }
}