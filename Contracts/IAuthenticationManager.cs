using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.DataTransferObjects;

namespace Contracts
{
    public interface IAuthenticationManager
    {
        Task<IList<string>?> ValidateUser(UserForAuthenticationDto userForAuth);
        
        Task<string> CreateToken();
    }
}