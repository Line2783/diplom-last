using System;
using System.Linq;
using System.Net;
using System.Net.Http;
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
using Microsoft.EntityFrameworkCore;

namespace diplom.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationManager _authManager;

        public AuthenticationController(ILoggerManager logger, IMapper mapper,
            UserManager<User> userManager, IAuthenticationManager authManager)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _authManager = authManager;
        }
        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="userForRegistration"></param>
        /// <returns></returns>
        [HttpPost("registrationUser")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto
            userForRegistration)
        {
            var user = _mapper.Map<User>(userForRegistration);
            var result = await _userManager.CreateAsync(user,
                userForRegistration.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            await _userManager.AddToRolesAsync(user, userForRegistration.Roles);
            return StatusCode(201);
        }
        /// <summary>
        /// Регистрация компании
        /// </summary>
        /// <param name="companyyForRegistration"></param>
        /// <returns></returns>
        [HttpPost("registrationCompanyy")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterCompanyy([FromBody] CompanyyForRegistrationDto
            companyyForRegistration)
        {
            var user = _mapper.Map<User>(companyyForRegistration);
            var result = await _userManager.CreateAsync(user,
                companyyForRegistration.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            await _userManager.AddToRolesAsync(user, companyyForRegistration.Roles);
            return StatusCode(201);
        }
        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            var userRoles = await _authManager.ValidateUser(user);
            if (userRoles == null)
            {
                _logger.LogWarn($"{nameof(Authenticate)}: Authentication failed. Wrong Email or password.");
                return Unauthorized();
            }

            return Ok(new { Token = await _authManager.CreateToken(), Role = userRoles}); // TODO
        }

        /// <summary>
        /// Изменения пароля
        /// </summary>
        /// <param name="resetPasswordDto"></param>
        /// <returns></returns>
        [HttpPost("ChangePassword"), Authorize(Roles = "User, Companyy")]
        public async Task<IActionResult> ChangePassword(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            var result = await _userManager.ChangePasswordAsync(user, resetPasswordDto.CurrentPassword,
                resetPasswordDto.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest( new { Error = "Password change failed. Incorrect email or password" }); //todo
            }

            return NoContent(); 
        }

       


        /// <summary>
        /// Проверка авторизации пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet("CheckAuthorization")]
        public async Task<IActionResult> CheckAuthorization()
        {
            if (User.Identity.IsAuthenticated)
            {
                return NoContent();
            }

            return BadRequest( new { Error = "You are not authorized!" });
        }
        
        
        /// <summary>
        /// Редактирование почты пользователя по Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPut("ChangeEmail/{id}"), Authorize(Roles = "User, Companyy") ]
        
        public async Task<IActionResult> ChangeEmailByIdAsync(string id, string email)
        {
            var user = await _userManager.FindByIdAsync(id);
        
            user.Email = email;
            
            await _userManager.UpdateAsync(user);
        
            return NoContent();
        
        }
       
        /// <summary>
        /// Редактирование имя пользователя пользователя по Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpPut("ChangeUserName/{id}"), Authorize(Roles = "User, Companyy")]
        
        public async Task<IActionResult> ChangeUserNameByIdAsync(string id,  string userName)
        {
            var user = await _userManager.FindByIdAsync(id);
        
            user.UserName = userName;
            
            await _userManager.UpdateAsync(user);
        
            return NoContent();
        
        }
        
        
        
        
    }
}
    
    

