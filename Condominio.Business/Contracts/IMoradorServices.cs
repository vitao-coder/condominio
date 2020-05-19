using Condominio.Model;
using System.Collections.Generic;

namespace Condominio.Services.Contracts
{
    public interface IMoradorServices
    {
        public List<Morador> BuscarMoradoresPorApartamento(long idApartamento);

        public List<Morador> BuscarMoradoresPorFiltro(MoradorServices.FiltroBuscaMorador filtroBusca, string valorFiltro);

        public bool AdicionarMorador(Morador morador);

        public bool AlterarMorador(Morador morador);

        public bool ExcluirMorador(long idMorador);
    }
}
