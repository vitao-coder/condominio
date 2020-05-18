using System;
using System.Collections.Generic;
using System.Text;
using Condominio.Services.Contracts;
using Condominio.Repository.Contracts;
using Condominio.Model;

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
    }
}
