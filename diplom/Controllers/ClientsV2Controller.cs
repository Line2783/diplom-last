using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace diplom.Controllers
{
    [Route("api/clients")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
    public class ClientsV2Controller : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        public ClientsV2Controller(IRepositoryManager repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            var clients = await
                _repository.Client.GetAllClientsAsync(trackChanges:
                    false);
            return Ok(clients);
        }
    }
}
