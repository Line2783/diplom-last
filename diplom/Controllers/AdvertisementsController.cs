using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace diplom.Controllers
{
    [Route("api/advertisements")]
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

        }
    }
    

