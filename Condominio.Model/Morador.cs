using Condominio.BaseModel;
using System;

namespace Condominio.Model
{
    public class Morador : BaseEntity
    {
        public string NomeCompleto { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Fone { get; set; }
            
        public int Cpf { get; set; }

        public Apartamento Apartamento { get; set; }

    }
}
