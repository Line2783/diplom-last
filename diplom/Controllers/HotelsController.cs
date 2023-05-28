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
        // [Authorize]
        // [HttpGet(Name = "GetHotels11")]
        // public async Task<IActionResult> GetAllCompanies()
        // {
        //     var companies = await _repository.Companyy.GetAllCompaniesAsync(trackChanges: false);
        //     var companiesDto = _mapper.Map<IEnumerable<HotelDto>>(companies);
        //     return Ok(companiesDto);
        // }
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
        /// Редактирование профиля авторизированной компании 
        /// </summary>
        /// <param name="companyForUpdate"></param>
        /// <returns></returns>
        [HttpPut("profile")] // todo
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
        
        // /// <summary>
        // /// Получает список всех отелей
        // /// </summary>
        // /// <returns> Список отелей</returns>.
        // [HttpGet(Name = "GetHotels")]
        // public async Task<IActionResult> GetHotels()
        // {
        //     var hotels = await _repository.Companyy.GetAllHotelsAsync(trackChanges:
        //         false);
        //     var hotelsDto = _mapper.Map<IEnumerable<HotelDto>>(hotels);
        //
        //     return Ok(hotelsDto);
        //
        // }
        
        // /// <summary>
        // /// Получение информации одного отеля по Id
        // /// </summary>
        // [HttpGet("{id}", Name = "HotelById")]
        // public async Task<IActionResult> GetHotel(Guid id)
        // {
        //     var hotel = await _repository.Hotel.GetHotelAsync(id, trackChanges:
        //         false);
        //     if (hotel == null)
        //     {
        //         _logger.LogInfo($"Hotel with id: {id} doesn't exist in the database.");
        //         return NotFound();
        //     }
        //     else
        //     {
        //         var hotelDto = _mapper.Map<HotelDto>(hotel);
        //         return Ok(hotelDto);
        //     }
        // }
        //
        // /// <summary>
        // /// Изменение информации об отеле по ID
        // /// </summary>
        // /// <param name="id"></param>
        // /// <param name="hotel"></param>
        // /// <returns></returns>
        // [HttpPut("{id}"), Authorize(Roles = "Companyy")]
        // [ServiceFilter(typeof(ValidationFilterAttribute))]
        // [ServiceFilter(typeof(ValidateHotelExistsAttribute))]
        // public async Task<IActionResult> UpdateHotel(Guid id,
        //     [FromBody] HotelForUpdateDto hotel)
        // {
        //     var hotelEntity = HttpContext.Items["hotel"] as Hotel;
        //     _mapper.Map(hotel, hotelEntity);
        //     await _repository.SaveAsync();
        //     return NoContent();
        // }
        
        
    }
}
