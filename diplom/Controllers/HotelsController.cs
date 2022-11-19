using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace diplom.Controllers
{
    [Route("api/hotels")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _manager;
        private readonly IMapper _mapper;
    }
}
