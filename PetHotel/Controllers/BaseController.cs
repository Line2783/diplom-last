using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace diplom.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected Guid UserId
        {
            get
            {
                var value = User.Claims
                    .FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType)?.Value;
                if (value !=  null)
                    return Guid.Parse(value);

                throw new Exception("Undefined user");
            }
        }

    }
}