﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
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

        /// <summary>
        /// Создание объявления 
        /// </summary>
        [HttpPost(Name = "CreateAdvertisement")]
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
        [HttpDelete("{id}")]
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
        [HttpPut("{id}")]
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

        // public async Task<IActionResult> OnPostUploadAsync()
        // {
        //     using (var memoryStream = new MemoryStream())
        //     {
        //         await FileUpload.FormFile.CopyToAsync(memoryStream);
        //
        //         // Upload the file if less than 2 MB
        //         if (memoryStream.Length < 2097152)
        //         {
        //             var file = new AppFile()
        //             {
        //                 Content = memoryStream.ToArray()
        //             };
        //
        //             _repository.File.Add(file);
        //
        //             await _repository.SaveChangesAsync();
        //         }
        //         else
        //         {
        //             ModelState.AddModelError("File", "The file is too large.");
        //         }
        //     }
        //
        //     return Page();
        // }


        //     [Route("api/ImageAPI/UploadFiles")]
        //     [HttpPost]
        //     public HttpResponseMessage UploadFiles()
        //     {
        //         //Create the Directory.
        //         string path = HttpContext.Current.Server.MapPath("~/Uploads/");
        //         if (!Directory.Exists(path))
        //         {
        //             Directory.CreateDirectory(path);
        //         }
        //
        //         //Fetch the File.
        //         HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];
        //
        //         //Fetch the File Name.
        //         string fileName = Path.GetFileName(postedFile.FileName);
        //
        //         //Save the File.
        //         postedFile.SaveAs(path + fileName);
        //
        //         //Send OK Response to Client.
        //         return Request.CreateResponse(HttpStatusCode.OK, fileName);
        //     }
        //
        //     [HttpPost]
        //     [Route("api/ImageAPI/GetFiles")]
        //     public HttpResponseMessage GetFiles()
        //     {
        //         string path = HttpContext.Current.Server.MapPath("~/Uploads/");
        //
        //         //Fetch the Image Files.
        //         List<string> images = new List<string>();
        //
        //         //Extract only the File Names to save data.
        //         foreach (string file in Directory.GetFiles(path))
        //         {
        //             images.Add(Path.GetFileName(file));
        //         }
        //
        //         return Request.CreateResponse(HttpStatusCode.OK, images);
        //     }
        // }
        
        
        
    }
}
    

