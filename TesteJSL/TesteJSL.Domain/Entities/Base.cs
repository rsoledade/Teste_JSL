using System;

namespace TesteJSL.Domain.Entities
{
    public class Base
    {
        public int Id { get; set; }

        public Base()
        {
            DataCadastro = DateTime.Now;
        }

        public DateTime DataCadastro { get; set; }
    }
}
