using System;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace diplom.ActionFilters
{
    public class ValidateOrderForClientExistsAttribute: IAsyncActionFilter

    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        public ValidateOrderForClientExistsAttribute(IRepositoryManager
                repository,
            ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var trackChanges = (method.Equals("PUT") || method.Equals("PATCH")) ?
                true : false;
            var clientId = (Guid)context.ActionArguments["clientId"];
            var client = await _repository.Client.GetClientAsync(clientId,
                false);
            if (client == null)
            {
                _logger.LogInfo($"Client with id: {clientId} doesn't exist in the database.");
                return;
          //      context.Result = new NotFoundResult();
            }
            var id = (Guid)context.ActionArguments["id"];
            var order = await _repository.Order.GetOrderAsync(clientId, id, 
                trackChanges);
            if (order == null)
            {
                _logger.LogInfo($"Order with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            
            else
            {
                context.HttpContext.Items.Add("order", order);
                await next();
            }
        }  
    }
}