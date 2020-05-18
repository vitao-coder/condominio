using Condominio.Model;
using Condominio.Repository.Contracts;
using Condominio.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
