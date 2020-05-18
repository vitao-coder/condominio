using System;
using System.Collections.Generic;
using System.Text;
using Condominio.Model;
using Condominio.Repository.Contracts;
using Condominio.Services.Contracts;

namespace Condominio.Services
{
    public class BlocoServices : IBlocoServices
    {
        readonly IBaseRepository<Bloco> _blocoRepository;
        readonly IMainContext _mainContext;

        public BlocoServices(IBaseRepository<Bloco> blocoRepository, IMainContext mainContext)
        {
            _blocoRepository = blocoRepository;
            _mainContext = mainContext;
        }
    }
}
