using System;
using System.Collections.Generic;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace diplom.Controllers
{
    [Route("api/clients/{clientId}/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public OrdersController(IRepositoryManager repository, ILoggerManager
                logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetOrderForClient(Guid clientId)
        {
            var client = _repository.Client.GetClient(clientId, trackChanges: false);
            if (client == null)
            {
                _logger.LogInfo($"Company with id: {clientId} doesn't exist in the database.");
                return NotFound();
            }
        
            var ordersFromDb = _repository.Order.GetOrders(clientId,
                trackChanges: false);
            return Ok(ordersFromDb);
        }
    }
    
    
}