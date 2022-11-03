﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace diplom.Controllers
{
    [Route("api/clients")]
    [ApiController]
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
        [HttpGet]
        public IActionResult GetClients()
        {
            
            {
                var clients = _repository.Client.GetAllClients(trackChanges:
                    false);
                var clientsDto = _mapper.Map<IEnumerable<ClientDto>>(clients);
                return Ok(clientsDto);
            }
            
        }
        [HttpGet("{id}", Name = "CompanyById")]
        public IActionResult GetClient(Guid id)
        {
            var client = _repository.Client.GetClient(id, trackChanges: false);
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
        
        [HttpPost]
        public IActionResult CreateClient([FromBody] ClientForCreationDto client)
        {
            if (client == null)
            {
                _logger.LogError("ClientForCreationDto object sent from client is null.");
                return BadRequest("ClientForCreationDto object is null");
            }
            var clientEntity = _mapper.Map<Client>(client);
            _repository.Client.CreateClient(clientEntity);
            _repository.Save();
            var clientToReturn = _mapper.Map<ClientDto>(clientEntity);
            return CreatedAtRoute("ClientById", new { id = clientToReturn.Id },
                clientToReturn);
        }
        
        [HttpGet("collection/({ids})", Name = "ClientCollection")]
        public IActionResult GetClientCollection([ModelBinder(BinderType =
            typeof(ArrayModelBinder<>))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }
            var clientEntities = _repository.Client.GetByIds(ids, trackChanges: false);
            
            if (ids.Count() != clientEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }
            var companiesToReturn =
                _mapper.Map<IEnumerable<CompanyDto>>(clientEntities);
            return Ok(companiesToReturn);
        }
        
        [HttpPost("collection")]
        public IActionResult CreateClientCollection([FromBody]
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
            _repository.Save();
            var clientCollectionToReturn =
                _mapper.Map<IEnumerable<ClientDto>>(clientEntities);
            var ids = string.Join(",", clientCollectionToReturn.Select(c => c.Id));
            return CreatedAtRoute("ClientCollection", new { ids },
                clientCollectionToReturn);
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteClient(Guid id)
        {
            var client = _repository.Client.GetClient(id, trackChanges: false);
            if (client == null)
            {
                _logger.LogInfo($"Client with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Client.DeleteClient(client);
            _repository.Save();
            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateClient(Guid id, [FromBody] ClientForUpdateDto
            client)
        {
            if (client == null)
            {
                _logger.LogError("ClientForUpdateDto object sent from client is null.");
                return BadRequest("ClientForUpdateDto object is null");
            }
            var clientEntity = _repository.Client.GetClient(id, trackChanges: true);
            if (clientEntity == null)
            {
                _logger.LogInfo($"Client with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(client, clientEntity);
            _repository.Save();
            return NoContent();
        }
    }
}
