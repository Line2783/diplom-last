﻿using System;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace diplom.ActionFilters
{
    public class ValidateAdvertisementForHotelExistsAttribute : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public ValidateAdvertisementForHotelExistsAttribute(IRepositoryManager
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
            var trackChanges = (method.Equals("PUT") || method.Equals("PATCH")) ? true : false;
            var hotelId = (Guid)context.ActionArguments["hotelId"];
            var hotel = await _repository.Hotel.GetHotelAsync(hotelId,
                false);
            if (hotel == null)
            {
                _logger.LogInfo($"Hotel with id: {hotelId} doesn't exist in the database.");
                return;
                //   context.Result = new NotFoundResult();
            }

            var id = (Guid)context.ActionArguments["id"];
            var advertisement = await _repository.Advertisement.GetAdvertisementAsync(hotelId,
                trackChanges);
            if (advertisement == null)
            {
                _logger.LogInfo($"Advertisement with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }

            else
            {
                context.HttpContext.Items.Add("advertisement", advertisement);
                await next();
            }
        }
    }
}