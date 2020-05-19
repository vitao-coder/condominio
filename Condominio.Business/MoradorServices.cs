using Condominio.Model;
using Condominio.Repository.Contracts;
using Condominio.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condominio.Services
{
    public class MoradorServices : IMoradorServices
    {
        readonly IBaseRepository<Morador> _moradorRepository;
        readonly IMainContext _mainContext;

        public MoradorServices(IBaseRepository<Morador> moradorRepository, IMainContext mainContext)
        {
            _moradorRepository = moradorRepository;
            _mainContext = mainContext;
        }

        public List<Morador> BuscarMoradoresPorApartamento(long idApartamento)
        {
            return _moradorRepository.GetAsync(it => it.Apartamento.Id == idApartamento).Result.ToList();
        }

        public bool AdicionarMorador(Morador morador)
        {
            var taskAdicionar = _moradorRepository.AddAsync(morador);
            return taskAdicionar.IsCompletedSuccessfully;
        }
        public bool AlterarMorador(Morador morador)
        {
            var taskAlterar = _moradorRepository.UpdateAsync(morador);
            return taskAlterar.IsCompletedSuccessfully;
        }

        public bool ExcluirMorador(long idMorador)
        {
            var taskExcluir = _moradorRepository.DeleteAsync(idMorador);
            return taskExcluir.IsCompletedSuccessfully;
        }

        public List<Morador> BuscarMoradoresPorFiltro(FiltroBuscaMorador filtroBusca, string valorFiltro)
        {
            List<Morador> listRetorno = new List<Morador>();

            switch (filtroBusca)
            {
                case FiltroBuscaMorador.NomeCompletoOuParteDele:
                    listRetorno = _moradorRepository.GetAsync(it => it.NomeCompleto.Contains(valorFiltro)).Result.ToList();
                    break;
                case FiltroBuscaMorador.DataNascimento:
                    DateTime filtroData;
                    if (DateTime.TryParse(valorFiltro,out filtroData)) 
                        listRetorno = _moradorRepository.GetAsync(it => it.DataNascimento == filtroData).Result.ToList();
                    break;
                case FiltroBuscaMorador.Cpf:
                    int filtroCpf;
                    if(int.TryParse(valorFiltro,out filtroCpf)) 
                        listRetorno = _moradorRepository.GetAsync(it => it.Cpf == filtroCpf).Result.ToList();
                    break;
                default:                    
                    break;
            }
            return listRetorno;

        }

        public enum FiltroBuscaMorador
        {
            NomeCompletoOuParteDele,
            DataNascimento,
            Cpf
        }
    }
}
