using Condominio.Model;
using System.Collections.Generic;

namespace Condominio.Services.Contracts
{
    public interface IApartamentoServices
    {
        public List<Apartamento> ListarTodosApartamentos();
        public bool AdicionarApartamento(Apartamento apartamento);
        public bool AlterarApartamento(Apartamento apartamento);
        public bool ExcluirApartamento(long idApartamento);
    }
}
