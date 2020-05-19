using System;
using System.Collections.Generic;
using System.Text;
using Condominio.Services.Contracts;
using Condominio.Repository.Contracts;
using Condominio.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Condominio.Services
{
    public class ApartamentoServices : IApartamentoServices
    {
        readonly IBaseRepository<Apartamento> _apartamentoRepository;
        readonly IMainContext _mainContext;

        public ApartamentoServices(IBaseRepository<Apartamento> apartamentoRepository, IMainContext mainContext)
        {
            _apartamentoRepository = apartamentoRepository;
            _mainContext = mainContext;
        }

        public List<Apartamento> ListarTodosApartamentos()
        {
            return _apartamentoRepository.GetQueryable(it => it != null).Include(it => it.Moradores).Include(it=>it.Bloco).ToList();
        }

        public bool AdicionarApartamento(Apartamento apartamento)
        {
            var taskAdicionar = _apartamentoRepository.AddAsync(apartamento);            
            return taskAdicionar.IsCompletedSuccessfully;
        }

        public bool AlterarApartamento(Apartamento apartamento)
        {
            var taskAlterar = _apartamentoRepository.UpdateAsync(apartamento);
            return taskAlterar.IsCompletedSuccessfully;
        }

        public bool ExcluirApartamento(long idApartamento)
        {
            var taskExcluir = _apartamentoRepository.DeleteAsync(idApartamento);
            return taskExcluir.IsCompletedSuccessfully;
        }
    }
}
