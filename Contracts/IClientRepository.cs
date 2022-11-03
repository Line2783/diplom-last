using System;
using System.Collections.Generic;
using Entities.Models;

namespace Contracts
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAllClients(bool trackChanges);
        Client GetClient(Guid clientId, bool trackChanges);
        void CreateClient(Client client);
        IEnumerable<Client> GetByIds(IEnumerable<Guid> ids, bool trackChanges); 
        void DeleteClient(Client client);


        
    }
}