using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace diplom.Controllers
{
    [Route("api/hotels")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public HotelsController(IRepositoryManager repository, ILoggerManager
            logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Получает список всех отелей
        /// </summary>
        /// <returns> Список отелей</returns>.
        [HttpGet(Name = "GetHotels")]
        public async Task<IActionResult> GetHotels()
        {
            var hotels = await _repository.Hotel.GetAllHotelsAsync(trackChanges:
                false);
            var hotelsDto = _mapper.Map<IEnumerable<HotelDto>>(hotels);

            return Ok(hotelsDto);

        }
        [HttpGet("{id}", Name = "HotelById")]
        public async Task<IActionResult> GetHotel(Guid id)
        {
            var hotel = await _repository.Hotel.GetHotelAsync(id, trackChanges:
                false);
            if (hotel == null)
            {
                _logger.LogInfo($"Hotel with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var hotelDto = _mapper.Map<HotelDto>(hotel);
                return Ok(hotelDto);
            }
        }
    }
}
