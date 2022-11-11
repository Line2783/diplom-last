using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using diplom.ActionFilters;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        public async Task<IActionResult> GetOrdersForClient(Guid clientId,
            [FromQuery] OrderParameters orderParameters)
        {
            if (!orderParameters.ValidAgeRange)
                return BadRequest("Quantity age can't be less than min Quantity.");
            var client = await _repository.Client.GetClientAsync(clientId,
                trackChanges:
                false);
            if (client == null)
            {
                _logger.LogInfo($"Client with id: {clientId} doesn't exist in the database.");
                return NotFound();
            }
            var ordersFromDb = await _repository.Order.GetOrdersAsync(clientId, orderParameters,
                 trackChanges: false);
            Response.Headers.Add("X-Pagination",
                JsonConvert.SerializeObject(ordersFromDb.MetaData));
            var ordersDto = _mapper.Map<IEnumerable<OrderDto>>(ordersFromDb);
            return Ok(ordersDto);
        }
        
        [HttpGet("{id}", Name = "GetOrdersForClient")]
        public async Task<IActionResult> GetOrderForClient(Guid clientId, Guid id)
        {
            var client = await _repository.Client.GetClientAsync(clientId, trackChanges: false);
            if (client == null)
            {
                _logger.LogInfo($"Client with id: {clientId} doesn't exist in the database.");
                return NotFound();
            }
        
            var orderDb = _repository.Client.GetClientAsync(clientId,  trackChanges: false);
            if (orderDb == null)
            {
                _logger.LogInfo($"Order with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var ordersDto = _mapper.Map<IEnumerable<OrderDto>>(orderDb);
            return Ok(ordersDto);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateOrderForClient(Guid clientId, [FromBody]
            OrderForCreationDto order)
        {
            if (order == null)
            {
                _logger.LogError("OrderForCreationDto object sent from client is null.");
                return BadRequest("OrderForCreationDto object is null");
            }
            
            var client = await _repository.Client.GetClientAsync(clientId, trackChanges: false);   
            if (client == null)
            {
                _logger.LogInfo($"Client with id: {clientId} doesn't exist in the database.");
                return NotFound();
            }
            var orderEntity = _mapper.Map<Order>(order);
            _repository.Order.CreateOrderForClient(clientId, orderEntity);
            await _repository.SaveAsync();
            var orderToReturn = _mapper.Map<OrderDto>(orderEntity);
            return CreatedAtRoute("GetOrderForClient", new
            {
                clientId, id = orderToReturn.Id
            }, orderToReturn);
        }
        
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateOrderForClientExistsAttribute))]
        public async Task<IActionResult> DeleteOrderForClient(Guid clientId, Guid id)
        {
            var orderForClient = HttpContext.Items["order"] as Order;
            _repository.Order.DeleteOrder(orderForClient);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateOrderForClientExistsAttribute))]
        public async Task<IActionResult> UpdateOrderForClient(Guid clientId, Guid id, [FromBody]
            OrderForUpdateDto order)
        {
            
            var orderEntity = HttpContext.Items["order"] as Order;

            _mapper.Map(order, orderEntity);
            await _repository.SaveAsync();
            return NoContent();
        } 
        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateOrderForCompany(Guid clientId, Guid id,
            [FromBody] JsonPatchDocument<OrderForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }
            var orderEntity = HttpContext.Items["order"] as Order;

            var orderToPatch = _mapper.Map<OrderForUpdateDto>(orderEntity);
            patchDoc.ApplyTo(orderToPatch, ModelState);
            TryValidateModel(orderToPatch);
            if(!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }
            _mapper.Map(orderToPatch, orderEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
    
    
}