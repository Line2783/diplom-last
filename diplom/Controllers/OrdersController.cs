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
        [HttpGet("{id}")]
        public IActionResult GetOrderForClient(Guid clientId, Guid id)
        {
            var client = _repository.Client.GetClient(clientId, trackChanges: false);
            if (client == null)
            {
                _logger.LogInfo($"Client with id: {clientId} doesn't exist in the database.");
                return NotFound();
            }
        
            var orderDb = _repository.Order.GetOrder(clientId, id, trackChanges: false);
            if (orderDb == null)
            {
                _logger.LogInfo($"Order with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var ordersDto = _mapper.Map<IEnumerable<OrderDto>>(orderDb);
            return Ok(ordersDto);
        }
        
        [HttpPost]
        public IActionResult CreateOrderForClient(Guid clientId, [FromBody]
            OrderForCreationDto order)
        {
            if (order == null)
            {
                _logger.LogError("OrderForCreationDto object sent from client is null.");
                return BadRequest("OrderForCreationDto object is null");
            }
            var client = _repository.Client.GetClient(clientId, trackChanges: false);
            if (client == null)
            {
                _logger.LogInfo($"Client with id: {clientId} doesn't exist in the database.");
                return NotFound();
            }
            var orderEntity = _mapper.Map<Order>(order);
            _repository.Order.CreateOrderForClient(clientId, orderEntity);
            _repository.Save();
            var orderToReturn = _mapper.Map<OrderDto>(orderEntity);
            return CreatedAtRoute("GetOrderForClient", new
            {
                clientId, id = orderToReturn.Id
            }, orderToReturn);
        }
    }
    
    
}