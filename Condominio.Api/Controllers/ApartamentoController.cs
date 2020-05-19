using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Condominio.Services.Contracts;
using Condominio.Repository.Contracts;
using Condominio.Model;

namespace Condominio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartamentoController : ControllerBase
    {
        readonly IApartamentoServices _apartamentoServices;
        readonly IMainContext _mainContext;


        public ApartamentoController(IApartamentoServices apartamentoServices, IMainContext mainContext)
        {
            _apartamentoServices = apartamentoServices;
            _mainContext = mainContext;
        }

        // GET: api/Apartamento
        [HttpGet]
        public List<Apartamento> Get()
        {
            return _apartamentoServices.ListarTodosApartamentos();
        }

        // GET: api/Apartamento/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(long id)
        {
            return "value";
        }

        // POST: api/Apartamento
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Apartamento/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
