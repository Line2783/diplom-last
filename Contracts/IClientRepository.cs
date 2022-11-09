using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllClientsAsync(bool trackChanges);
        Task<Client> GetClientAsync(Guid clientId, bool trackChanges);
        Task<IEnumerable<Client>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges); 
        void CreateClient(Client client);
        void DeleteClient(Client client);


        
    }
}