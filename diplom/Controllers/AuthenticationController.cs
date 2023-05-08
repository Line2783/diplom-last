﻿using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using diplom.ActionFilters;
using Entities.DataTransferObjects;
using Entities.Models;
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

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto
            user)
        {
            if (!await _authManager.ValidateUser(user))
            {
                _logger.LogWarn($"{nameof(Authenticate)}: Authentication failed. Wrong Email or password.");
                return Unauthorized();
            }

            return Ok(new { Token = await _authManager.CreateToken(), });
        }

        
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ResetPasswordDto resetPasswordDto)
        {           
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);          
            var result = await _userManager.ChangePasswordAsync(user, resetPasswordDto.CurrentPassword, resetPasswordDto.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok();
        }
        
        // public HttpResponseMessage Get()
        // {
        //     if (User.Identity.IsAuthenticated)
        //     {
        //         authMessage = $"{User.Identity.Name} is authenticated.";
        //         claims = user.Claims;
        //         surname = user.FindFirst(c => c.Type == ClaimTypes.Surname)?.Value;
        //     }
        //     else
        //     {
        //         authMessage = "The user is NOT authenticated.";
        //     }
        //
        // }
        
        
        /// <summary>
        /// Проверка авторизации пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet("CheckAuthorization")]
        public async Task<IActionResult> CheckAuthorization()
        {           
            if (User.Identity.IsAuthenticated)
            {
                return Ok();
            }
            return BadRequest();
        }
        
        
        
       
       
            
        
        
        
    }
}
    
    

