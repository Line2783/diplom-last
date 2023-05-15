using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using diplom.ActionFilters;
using diplom.Service;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace diplom.Controllers
{
    [Route("api/hotels/advertisements")]
    [ApiController]
    public class AdvertisementsController : BaseController
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

        /// <summary>
        /// Создание объявления 
        /// </summary>
        [HttpPost(Name = "CreateAdvertisement"), Authorize(Roles = "Companyy")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateAdvertisement([FromBody] AdvertisementForCreationDto
            advertisement)
        {
            var advertisementEntity = _mapper.Map<Advertisement>(advertisement);
            _repository.Advertisement.CreateAdvertisement(advertisementEntity);
            await _repository.SaveAsync();
            var advertisementToReturn = _mapper.Map<AdvertisementDto>(advertisementEntity);
            return CreatedAtRoute("AdvertisementById", new { id = advertisementToReturn.Id },
                advertisementToReturn);
        }

        /// <summary>
        /// Удаление объявления 
        /// </summary>
        [HttpDelete("{id}"), Authorize(Roles = "Companyy")]
        [ServiceFilter(typeof(ValidateAdvertisementExistsAttribute))]
        public async Task<IActionResult> DeleteAdvertisement(Guid id)
        {
            var advertisement = HttpContext.Items["advertisement"] as Advertisement;
            _repository.Advertisement.DeleteAdvertisement(advertisement);
            await _repository.SaveAsync();
            return NoContent();
        }

        /// <summary>
        /// Редактирование объявления 
        /// </summary>
        [HttpPut("{id}"), Authorize(Roles = "Companyy")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateAdvertisementExistsAttribute))]
        public async Task<IActionResult> UpdateAdvertisement(Guid id,
            [FromBody] AdvertisementForUpdateDto advertisement)
        {
            var advertisementEntity = HttpContext.Items["advertisement"] as Advertisement;
            _mapper.Map(advertisement, advertisementEntity);
            await _repository.SaveAsync();
            return NoContent();
        }

        
        
        /// <summary>
        /// Добавление изображения для объявления
        /// </summary>
        /// <param name="idAdvertisement"></param>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        [HttpPost("{idAdvertisement}"), Authorize(Roles = "Companyy")]
        public async Task<IActionResult> AddPhoto(Guid idAdvertisement, IFormFile uploadedFile)
        {
            var productPhoto1 = new ProductPhoto
            {
                Id = Guid.NewGuid(),
                AdvertisementId = idAdvertisement,
                Name = uploadedFile.FileName
            };
            using (var binaryReader = new BinaryReader(uploadedFile.OpenReadStream()))
            {
                productPhoto1.Photo = binaryReader.ReadBytes((int)uploadedFile.Length);
            }

            await _repository.ProductPhoto.SaveFilesAsync(productPhoto1);
            await _repository.ProductPhoto.SaveRepositoryAsync();

            return await GetPhoto(productPhoto1.Id);
        }

        /// <summary>
        /// Получение изображения объявления
        /// </summary>
        /// <param name="imageId">id фото</param>
        /// <returns></returns>
        [HttpGet("/photo/{imageId}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(Guid imageId)
        {
            var file = await _repository.ProductPhoto.GetFileAsync(imageId, false);
            var stream = new MemoryStream(file.Photo);
            return File(stream, "application/octet-stream", $"{file.Name}");
        }
        
        
        
    }
}
    

