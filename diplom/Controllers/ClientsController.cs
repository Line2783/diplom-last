using System;
using System.Linq;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace diplom.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        public ClientsController(IRepositoryManager repository, ILoggerManager
            logger)
        {
            _repository = repository;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult GetClients()
        {
            try
            {
                var companies = _repository.Client.GetAllClients(trackChanges:
                    false);
                var companiesDto = companies.Select(c => new ClientDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    AddressAge = string.Join(' ', c.Address, c.Age)
                }).ToList();
                return Ok(companiesDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetClients)}action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
