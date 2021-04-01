using System;
using System.Collections.Generic;
using System.Text;

namespace MapperTests.ViewModel
{
    public class ClienteViewModel
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string NomeCompleto { get; set; }
        public DateTime DataNasc { get; set; }
        public bool Ativo { get; set; }
        public int CodigoContrato { get; set; }
        public int Quantidade { get; set; }
    }
}
