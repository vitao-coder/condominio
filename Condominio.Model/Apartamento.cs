using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Condominio.BaseModel;

namespace Condominio.Model
{
    public class Apartamento : BaseEntity
    {
        public int Numero { get; set; }

        public Bloco Bloco { get; set; }

        public List<Morador> Moradores { get; set; }
    }
}
