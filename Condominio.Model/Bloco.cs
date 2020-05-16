using Condominio.BaseModel;
using System.Collections.Generic;

namespace Condominio.Model
{
    public class Bloco : BaseEntity
    {
        public string Descricao { get; set; }

        public List<Apartamento> Apartamentos { get; set; }
    }
}
