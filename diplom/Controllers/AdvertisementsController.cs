using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
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
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;

        public AdvertisementsController(IRepositoryManager repository, ILoggerManager logger,  UserManager<User> userManager,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
           _userManager = userManager;

        }

        /// <summary>
        /// Создание объявления
        /// </summary>
        /// <param name="advertisement"></param>
        /// <returns></returns>
         //work work todo
        [HttpPost("create-advertisement"), Authorize(Roles = "Companyy")]
        public async Task<IActionResult> CreateAdvertisement([FromBody] AdvertisementForCreationDto advertisement)
        {
            try
            {
                // Get UserId from base controller
                Guid userId = UserId;
        
                // Check if user exists
                User user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                {
                    return NotFound("User not found");
                }
                var advertisementEntity = _mapper.Map<Advertisement>(advertisement);

                // Set Advertisement User Id
                advertisementEntity.CompanyId = userId.ToString();
        
                _repository.Advertisement.CreateAdvertisement(advertisementEntity);
                await _repository.SaveAsync();
                var advertisementToReturn = _mapper.Map<AdvertisementDto>(advertisementEntity);
                return  CreatedAtRoute("AdvertisementById", new { id = advertisementToReturn.AdvertisementId },
                   advertisementToReturn);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to create advertisement: {ex.Message}");
            }
        }
        /// <summary>
        /// Редактирование объявления по Id объявления
        /// </summary>
        /// <param name="id"></param>
        /// <param name="advertisement"></param>
        /// <returns></returns>
         // work work todo
        [HttpPut("advertisement/{id}"), Authorize(Roles = "Companyy")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateAdvertisementExistsAttribute))]
        public async Task<IActionResult> UpdateAdvertisement1(Guid id, [FromBody] AdvertisementForUpdateDto advertisement)
        {
            try
            {
                Guid userId = UserId;

                // Check if Advertisement exists
                var advertisementEntity = HttpContext.Items["advertisement"] as Advertisement;


                // Check if Advertisement belongs to User
                if (advertisementEntity.CompanyId != userId.ToString())
                {
                    return Forbid();
                }
                _mapper.Map(advertisement, advertisementEntity);
                await _repository.SaveAsync();
                return NoContent();
               
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to update advertisement: {ex.Message}");
            }
        }
        /// <summary>
        /// Удаление объявления по Id объявления
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // work work todo
        [Authorize]
        [HttpDelete("advertisement/{id}"), Authorize(Roles = "Companyy")]
        [ServiceFilter(typeof(ValidateAdvertisementExistsAttribute))]
        public async Task<IActionResult> DeleteAdvertisement1(Guid id)
        {
            try
            {
                Guid userId = UserId;

                // Check if Advertisement exists
                var advertisementEntity = HttpContext.Items["advertisement"] as Advertisement;


                // Check if Advertisement belongs to User
                if (advertisementEntity.CompanyId != userId.ToString())
                {
                    return Forbid();
                }

                // Delete Advertisement
                _repository.Advertisement.DeleteAdvertisement(advertisementEntity);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete advertisement: {ex.Message}");
            }
        }
        /// <summary>
        /// Получение всех объявлений компании по CompanyId
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        [HttpGet("{companyId}/advertisements")] // work todo
        public async Task<IActionResult> GetAllAdvertisementsForCompany(string companyId)
        {
            var advertisements = await _repository.Advertisement.GetAdvertisementsForCompanyAsync(companyId, trackChanges: false);
            
            var advetrisemntsDto = _mapper.Map<IEnumerable<AdvertisementDto>>(advertisements);

            foreach (var item in advetrisemntsDto)
            {
                var photos =
                    await    _repository.ProductPhoto.GetAllProductPhotoAsync(item.AdvertisementId, trackChanges: false);
                item.Photos = photos.Select(x=>x.Id.ToString()).ToList();
            }   

            return Ok(advetrisemntsDto);
        }
        
        
        /// <summary>
        /// Получает список всех объявлений
        /// </summary>
        /// <returns> Список объявлений</returns>.
        [HttpGet(Name = "GetAdvertisements")]  // todo
        public async Task<IActionResult> GetAdvertisements()
        {
            
            var advetrisemnts = await _repository.Advertisement.GetAllAdvertisementsAsync(trackChanges:
                false);
            
            var advetrisemntsDto = _mapper.Map<IEnumerable<AdvertisementDto>>(advetrisemnts);

            foreach (var item in advetrisemntsDto)
            {
                var photos =
                await    _repository.ProductPhoto.GetAllProductPhotoAsync(item.AdvertisementId, trackChanges: false);
                item.Photos = photos.Select(x=>x.Id.ToString()).ToList();
            }

            return Ok(advetrisemntsDto);
        }
        

        /// <summary>
        /// Получение информации одного объявления по Id(объявления)
        /// </summary>
        [HttpGet("{id}", Name = "AdvertisementById")] // todo
        public async Task<IActionResult> GetAdvertisement(Guid id)
        {
            var advertisement = await _repository.Advertisement.GetAdvertisementAsync(id, trackChanges:
                false);
            if (advertisement == null)
            {
                _logger.LogInfo($"Advertisement with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var advertisementDto = _mapper.Map<AdvertisementDto>(advertisement);
            var photos =
                await    _repository.ProductPhoto.GetAllProductPhotoAsync(id, trackChanges: false);
            advertisementDto.Photos = photos.Select(x=>x.Id.ToString()).ToList();
            return Ok(advertisementDto);
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
    

