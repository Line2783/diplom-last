using System.Threading.Tasks;
using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private ICompanyRepository _companyRepository;
        private IEmployeeRepository _employeeRepository;
        private IClientRepository _clientRepository;
        private IOrderRepository _orderRepository;
        
        private IHotelRepository _hotelRepository;
        private IAdvertisementRepository _advertisementRepository;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public ICompanyRepository Company
        {
            get
            {
                if (_companyRepository == null)
                    _companyRepository = new CompanyRepository(_repositoryContext);
                return _companyRepository;
            }
        }
        public IEmployeeRepository Employee
        {
            get
            {
                if (_employeeRepository == null)
                    _employeeRepository = new EmployeeRepository(_repositoryContext);
                return _employeeRepository;
            }
        }
        public IClientRepository Client
        {
            get
            {
                if (_clientRepository == null)
                    _clientRepository = new ClientRepository(_repositoryContext);
                return _clientRepository;
            }
        }
        public IOrderRepository Order
        {
            get
            {
                if (_orderRepository == null)
                    _orderRepository = new OrderRepository(_repositoryContext);
                return _orderRepository;
            }
        }


        public IHotelRepository Hotel
        {
            get
            {
                if (_hotelRepository == null)
                    _hotelRepository = new HotelRepository(_repositoryContext);
                return _hotelRepository;
            }
        }

        public IAdvertisementRepository Advertisement
        {
            get
            {
                if (_advertisementRepository == null)
                    _advertisementRepository = new AdvertisementRepository(_repositoryContext);
                return _advertisementRepository;
            }
        }
        
        public void Save() => _repositoryContext.SaveChanges();
        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();

    }
}