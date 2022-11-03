using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public IEnumerable<Client> GetAllClients(bool trackChanges) =>
            FindAll(trackChanges)
                .OrderBy(c => c.Name)
                .ToList();
        
        public Client GetClient(Guid clientId, bool trackChanges) => FindByCondition(c 
            => c.Id.Equals(clientId), trackChanges) .SingleOrDefault();
        public void CreateClient(Client client) => Create(client);
    }
}