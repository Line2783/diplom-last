using System.Collections.Generic;
using Entities.Models;

namespace Contracts
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAllClients(bool trackChanges);
    }
}