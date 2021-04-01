using System;
using System.Collections.Generic;
using System.Text;

namespace MapperTests.Entities
{
    public class Cliente
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNasc { get; set; }
        public bool Ativo { get; set; }
        public int CodContrato { get; set; }
        public int Qtde { get; set; }
    }
}
