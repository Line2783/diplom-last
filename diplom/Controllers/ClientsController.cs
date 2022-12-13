using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using diplom.ActionFilters;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace diplom.Controllers
{
    [Route("api/clients")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class ClientsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public ClientsController(IRepositoryManager repository, ILoggerManager
            logger,  IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        
        
        /// <summary>
        /// Получает список всех компаний
        /// </summary>
        /// <returns> Список компаний</returns>.
        [HttpGet(Name = "GetClients"), Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetClients()
        {
            
            {
                var clients = await _repository.Client.GetAllClientsAsync(trackChanges:
                    false);
                var clientsDto = _mapper.Map<IEnumerable<ClientDto>>(clients);
                return Ok(clientsDto);
            }
            
        }
        /// <summary>
        /// Получает клиента по Id
        /// </summary>
        [HttpGet("{id}", Name = "ClientById")]
        public async Task<IActionResult> GetClient(Guid id)
        {
            var client = await _repository.Client.GetClientAsync(id, trackChanges: false);
            if (client == null)
            {
                _logger.LogInfo($"Client with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var clientDto = _mapper.Map<ClientDto>(client);
                return Ok(clientDto);
            }
        }
        /// <summary>
        /// Создает клиента
        /// </summary>
        /// <param name="company"></param>.
        /// <returns>Созданный клиент</returns>.
        /// <response code="201"> Возвращает только что созданный элемент</response>.
        /// <response code="400"> Если элемент равен null</response>.
        /// <код ответа="422"> Если модель недействительна</ответ>.
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateClient([FromBody] ClientForCreationDto client)
        {
            
            var clientEntity = _mapper.Map<Client>(client);
            _repository.Client.CreateClient(clientEntity);
            await _repository.SaveAsync();
            var clientToReturn = _mapper.Map<ClientDto>(clientEntity);
            return CreatedAtRoute("ClientById", new { id = clientToReturn.Id },
                clientToReturn);
        }
        /// <summary>
        /// Получение новой коллекции
        /// </summary>
        [HttpGet("collection/({ids})", Name = "ClientCollection")]
        public async Task<IActionResult> GetClientCollection([ModelBinder(BinderType =
            typeof(ArrayModelBinder<>))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }
            var clientEntities = await _repository.Client.GetByIdsAsync(ids, trackChanges: false);
            
            if (ids.Count() != clientEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }
            var companiesToReturn =
                _mapper.Map<IEnumerable<CompanyDto>>(clientEntities);
            return Ok(companiesToReturn);
        }
        /// <summary>
        /// Создает нового клиента и его заказ(коллекция)
        /// </summary>
        [HttpPost("collection")]
        public async Task<IActionResult> CreateClientCollection([FromBody]
            IEnumerable<ClientForCreationDto> clientCollection)
        {
            if (clientCollection == null)
            {
                _logger.LogError("Client collection sent from client is null.");
                return BadRequest("Client collection is null");
            }
            var clientEntities = _mapper.Map<IEnumerable<Client>>(clientCollection);
            foreach (var client in clientEntities)
            {
                _repository.Client.CreateClient(client);
            }
            await _repository.SaveAsync();
            var clientCollectionToReturn =
                _mapper.Map<IEnumerable<ClientDto>>(clientEntities);
            var ids = string.Join(",", clientCollectionToReturn.Select(c => c.Id));
            return CreatedAtRoute("ClientCollection", new { ids },
                clientCollectionToReturn);
        }
        /// <summary>
        /// Удаление клиента
        /// </summary>
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateClientExistsAttribute))]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            var client = HttpContext.Items["client"] as Client;

            _repository.Client.DeleteClient(client);
            await _repository.SaveAsync();
            return NoContent();
        }
        /// <summary>
        /// Изменение клиента
        /// </summary>
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateClientExistsAttribute))]
        public async Task<IActionResult> UpdateClient(Guid id, [FromBody] ClientForUpdateDto
            client)
        {
            var clientEntity = HttpContext.Items["client"] as Client;
            _mapper.Map(client, clientEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
        
        [HttpOptions]
        public IActionResult GetClientsOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST");
            return Ok();
        }
    }
}
