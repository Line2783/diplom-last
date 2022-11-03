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
    }
}