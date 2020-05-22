using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Condominio.Model;
using Condominio.Repository.Contracts;
using Condominio.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Condominio.Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class BlocoController : ControllerBase
    {

        readonly IBlocoServices _blocoServices;
        readonly IMainContext _mainContext;

        public BlocoController(IBlocoServices blocoServices, IMainContext mainContext)
        {
            _blocoServices = blocoServices;
            _mainContext = mainContext;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return Ok(_blocoServices.ListarTodosBlocos());
        }
    }
}