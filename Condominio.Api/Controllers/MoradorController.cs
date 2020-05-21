using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Condominio.Services.Contracts;
using Condominio.Repository.Contracts;
using Condominio.Model;
using Condominio.Services;

namespace Condominio.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoradorController : ControllerBase
    {
        readonly IMoradorServices _moradorServices;
        readonly IMainContext _mainContext;

        public MoradorController(IMoradorServices moradorServices, IMainContext mainContext)
        {
            _moradorServices = moradorServices;
            _mainContext = mainContext;
        }
                
        [HttpGet("get-moradores-por-apto/{idApto}")]
        public List<Morador> GetMoradoresPorApto(long idApto)
        {
            return _moradorServices.BuscarMoradoresPorApartamento(idApto);
        }

        [HttpGet("get-moradores-por-filtro/{idApto}")]
        public List<Morador> GetMoradoresPorApto(MoradorServices.FiltroBuscaMorador filtro, string valorFiltro)
        {
            return _moradorServices.BuscarMoradoresPorFiltro(filtro, valorFiltro);
        }

        [HttpPost("post")]
        public bool Post([FromBody]Morador moradorAdicionar)
        {
            return _moradorServices.AdicionarMorador(moradorAdicionar);
        }
        
        [HttpPut("put")]
        public bool Put([FromBody] Morador moradorAlterar)
        {
            _mainContext.GetSet<Morador>().Attach(moradorAlterar);
            return _moradorServices.AlterarMorador(moradorAlterar);
        }
                
        [HttpDelete("delete/{id}")]
        public bool Delete(long id)
        {
            return _moradorServices.ExcluirMorador(id);
        }
    }
}
