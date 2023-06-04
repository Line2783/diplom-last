using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using diplom.ActionFilters;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace diplom.Controllers
{
    [Route("api/hotels")]
    [ApiController]
    public class HotelsController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
    
        public HotelsController(IRepositoryManager repository, ILoggerManager
            logger, IMapper mapper, UserManager<User> userManager)
        {
            _userManager = userManager;

            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Получение всех компаний
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetAllCompanies")] //work work todo
        public async Task<IActionResult> GetAllCompanies()
        {
            var users = await _repository.User.GetAllCompaniesAsync(trackChanges: false);
            var usersWithHotels = users.Where(u => !string.IsNullOrEmpty(u.HotelName));
            var usersDto = _mapper.Map<IEnumerable<CompanyDto>>(usersWithHotels);
            return Ok(usersDto);
        }   
        /// <summary>
        /// Получение компании по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}",Name = "GetCompanyById")] // work work todo
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userDto = _mapper.Map<CompanyDto>(user);

            return Ok(userDto);
        }
        /// <summary>
        /// Получение информации авторизованной компании
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAutorizeCompany"), Authorize(Roles = "Companyy")]
        public async Task<IActionResult> GetCurrentCompany()
        {
            Guid userId = UserId;
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return NotFound();
            }

            var userDto = _mapper.Map<CompanyDto>(user);

            return Ok(userDto);
        } 
        
        /// <summary>
        /// Редактирование профиля авторизированной компании 
        /// </summary>
        /// <param name="companyForUpdate"></param>
        /// <returns></returns>
        [HttpPut("profile"), Authorize(Roles = "Companyy")] // todo
        public async Task<IActionResult> UpdateProfile([FromBody] CompanyForUpdateDto companyForUpdate)
        {
            Guid userId = UserId;
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return NotFound();
            }

            _mapper.Map(companyForUpdate, user);
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return NoContent();
        }
        
        /// <summary>
        /// Получение всех объявлений авторизированной компании
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAutorizeAdv"), Authorize(Roles = "Companyy")] // work todo
        public async Task<IActionResult> GetAllAdvertisementsForCompany()
        {
            Guid companyId = UserId;
            var advertisements = await _repository.Advertisement.GetAdvertisementsForCompanyAsync(companyId.ToString(), trackChanges: false);
            
            var advetrisemntsDto = _mapper.Map<IEnumerable<AdvertisementDto>>(advertisements);

            foreach (var item in advetrisemntsDto)
            {
                var photos =
                    await    _repository.ProductPhoto.GetAllProductPhotoAsync(item.AdvertisementId, trackChanges: false);
                item.Photos = photos.Select(x=>x.Id.ToString()).ToList();
            }   

            return Ok(advetrisemntsDto);
        }
    }
}
