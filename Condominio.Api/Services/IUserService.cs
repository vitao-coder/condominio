using Condominio.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Condominio.Api.Services
{
    public interface IUserService
    {
        public User Authenticate(string usuario, string senha);
    }
}
