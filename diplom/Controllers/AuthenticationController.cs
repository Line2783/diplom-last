using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using diplom.ActionFilters;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        // [HttpPost]
        // [HttpPost]
        // public async Task<IActionResult> ChangePassword(ChangePasswordDto model)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         User user = await _userManager.FindByIdAsync(model.Email);
        //         if (user != null)
        //         {
        //             IdentityResult result =
        //                 await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        //             if (result.Succeeded)
        //             {
        //                 return RedirectToAction("");
        //             }
        //             else
        //             {
        //                 foreach (var error in result.Errors)
        //                 {
        //                     ModelState.AddModelError(string.Empty, error.Description);
        //                 }
        //             }
        //         }
        //         else
        //         {
        //             ModelState.AddModelError(string.Empty, "Пользователь не найден");
        //         }
        //     }
        //
        //     return StatusCode(201);
        // }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
        
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
                return BadRequest("Invalid Request");
        
            var resetPassResult = await _userManager.ChangePasswordAsync(user, resetPasswordDto.CurrentPassword, resetPasswordDto.NewPassword);
            if (!resetPassResult.Succeeded)
            {
                var errors = resetPassResult.Errors.Select(e => e.Description);
        
                return BadRequest(new { Errors = errors });
            }
        
            return Ok();
        }
        [HttpPost("ResetPassword1")]
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
        
        [HttpPost("ResetPassword3")]
        public async Task<IActionResult> ChangePassword3(ResetPasswordDto resetPasswordDto)
        {           
            if (!ModelState.IsValid)
                return BadRequest();
        
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);          
            if (user == null)
                return BadRequest("Invalid Request");
        
            var result = await _userManager.ChangePasswordAsync(user, resetPasswordDto.CurrentPassword, resetPasswordDto.NewPassword);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
        
                return BadRequest(new { Errors = errors });
            }
        
            return Ok();
        }
        
        [HttpPost("ResetPassword4")]
        public async Task<IActionResult> ChangePassword4(ResetPasswordDto resetPasswordDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);          
                if (user != null)
                {
                    var result = await _userManager.ChangePasswordAsync(user, resetPasswordDto.CurrentPassword, resetPasswordDto.NewPassword);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }

            return StatusCode(201);
        }
    }
}
    
    

