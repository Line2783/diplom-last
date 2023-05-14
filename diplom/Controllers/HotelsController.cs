using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using diplom.ActionFilters;
using Entities.DataTransferObjects;
using Entities.Models;
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
        
        /// <summary>
        /// Получение информации одного отеля по Id
        /// </summary>
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
        
        /// <summary>
        /// Изменение информации об отеле по ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hotel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateHotelExistsAttribute))]
        public async Task<IActionResult> UpdateHotel(Guid id,
            [FromBody] HotelForUpdateDto hotel)
        {
            var hotelEntity = HttpContext.Items["hotel"] as Hotel;
            _mapper.Map(hotel, hotelEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
        
        /// <summary>
        /// Добавление изображения для отеля
        /// </summary>
        /// <param name="idHotel"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        [HttpPost("{idHotel}")]
        public async Task<IActionResult> AddPhoto(Guid idHotel, IFormFile uploadedFile)
        {
            var productPhoto1 = new HotelPhoto
            {
                Id = Guid.NewGuid(),
                HotelId = idHotel,
                Name = uploadedFile.FileName
            };
            using (var binaryReader = new BinaryReader(uploadedFile.OpenReadStream()))
            {
                productPhoto1.Photo = binaryReader.ReadBytes((int)uploadedFile.Length);
            }

            await _repository.HotelPhoto.SaveFilesAsync(productPhoto1);
            await _repository.HotelPhoto.SaveRepositoryAsync();

            return await GetPhoto(productPhoto1.Id);
        }

        /// <summary>
        /// Получение изображения отеля
        /// </summary>
        /// <param name="imageId">id фото</param>
        /// <returns></returns>
        [HttpGet("/HotelPhoto/{imageId}", Name = "GetHotelPhoto")]
        public async Task<IActionResult> GetPhoto(Guid imageId)
        {
            var file = await _repository.HotelPhoto.GetFileAsync(imageId, false);
            var stream = new MemoryStream(file.Photo);
            return File(stream, "application/octet-stream", $"{file.Name}");
        }
    }
}
