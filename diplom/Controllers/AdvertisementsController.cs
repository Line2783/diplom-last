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
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace diplom.Controllers
{
    [Route("api/hotels/advertisements")]
    [ApiController]
    public class AdvertisementsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public AdvertisementsController(IRepositoryManager repository, ILoggerManager logger,
            IMapper mapper)

        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;

        }    
        
        /// <summary>
        /// Получает список всех объявлений
        /// </summary>
        /// <returns> Список объявлений</returns>.
        [HttpGet(Name = "GetAdvertisements")]
        public async Task<IActionResult> GetAdvertisements()
        {
            var advetrisemnts = await _repository.Advertisement.GetAllAdvertisementsAsync(trackChanges:
                false);
            var advetrisemntsDto = _mapper.Map<IEnumerable<AdvertisementDto>>(advetrisemnts);

            return Ok(advetrisemntsDto);

        }
        
        /// <summary>
        /// Получение информации одного объявления по Id
        /// </summary>
        [HttpGet("{id}", Name = "AdvertisementById")]
        public async Task<IActionResult> GetAdvertisement(Guid id)
        {
            var advertisement = await _repository.Advertisement.GetAdvertisementAsync(id, trackChanges:
                false);
            if (advertisement == null)
            {
                _logger.LogInfo($"Advertisement with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var advertisementDto = _mapper.Map<AdvertisementDto>(advertisement);
                return Ok(advertisementDto);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateAdvertisementForHotel(Guid hotelId, [FromBody]
            AdvertisementForCreationDto advertisement)
        {
            if (advertisement == null)
            {
                _logger.LogError("AdvertisementForCreationDto object sent from  is null.");
                return BadRequest("AdvertisementForCreationDto object is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the AdvertisementForCreationDto object");
                return UnprocessableEntity(ModelState);
            }
            var hotel = await _repository.Hotel.GetHotelAsync(hotelId, trackChanges: false);
            if (hotel == null)
            {
                _logger.LogInfo($"Hotel with id: {hotelId} doesn't exist in the database.");
                return NotFound();
            }
            var advertisementEntity = _mapper.Map<Advertisement>(advertisement);
            _repository.Advertisement.CreateAdvertisementForHotel(hotelId, advertisementEntity);
            await _repository.SaveAsync();
            var advertisementToReturn = _mapper.Map<AdvertisementDto>(advertisementEntity);
            return CreatedAtRoute("GetAdvertisementForHotel", new
            {
                hotelId, id = advertisementToReturn.Id
            }, advertisementToReturn);
        }

    }
}
    

