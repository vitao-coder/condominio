using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Condominio.Services.Contracts;
using Condominio.Repository.Contracts;
using Condominio.Model;
using Microsoft.AspNetCore.Authorization;

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
                
        [HttpGet]
        [Authorize]
        public List<Apartamento> Get()
        {
            return _apartamentoServices.ListarTodosApartamentos();
        }
                
        [HttpPost("post")]
        [Authorize]
        public bool Post([FromBody]Apartamento apartamentoAdicionar)
        {
            return _apartamentoServices.AdicionarApartamento(apartamentoAdicionar);
        }
        
        [HttpPut("put")]
        [Authorize]
        public bool Put([FromBody] Apartamento apartamentoAlterar)
        {
            _mainContext.GetSet<Apartamento>().Attach(apartamentoAlterar);
            return _apartamentoServices.AlterarApartamento(apartamentoAlterar);
        }
                
        [HttpDelete("delete/{id}")]
        [Authorize]
        public bool Delete(long id)
        {
            return _apartamentoServices.ExcluirApartamento(id);
        }
    }
}
