using Condominio.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Condominio.Services.Contracts
{
    public interface IBlocoServices
    {
        public List<Bloco> ListarTodosBlocos();
    }
}
