using Mapper;
using MapperTests.Entities;
using MapperTests.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MapperTests
{
    public class MapperTests
    {
        private readonly IMapper _mapper = null;
        public MapperTests()
        {
            _mapper = new Mapper.Mapper();
        }

        [Fact]
        public void MapperClienteToClienteViewModel()
        {
            var cliente = new Cliente()
            {
                ID = 1,
                Ativo = true,
                DataNasc = DateTime.Now.AddYears(-30),
                Nome = "Ana",
                Sobrenome = "Silva",
                CodContrato = 1001,
                Qtde = 22
            };

            _mapper
                .SetConfig<Cliente, ClienteViewModel>(x => x.CodContrato, x => x.CodigoContrato)
                .SetConfig<Cliente, ClienteViewModel>(x => x.Qtde, x => x.Quantidade)
                .SetConfig<Cliente, ClienteViewModel>(x => x.Nome, x => x.NomeCompleto, x => x.Sobrenome, " - ");

            var viewmodel = _mapper.Map<Cliente, ClienteViewModel>(cliente);

            Assert.Equal(cliente.ID, viewmodel.ID);
            Assert.Equal(cliente.Ativo, viewmodel.Ativo);
            Assert.Equal(cliente.DataNasc, viewmodel.DataNasc);
            Assert.Equal(cliente.Nome, viewmodel.Nome);
            Assert.Equal(cliente.Sobrenome, viewmodel.Sobrenome);
            Assert.Equal(cliente.CodContrato, viewmodel.CodigoContrato);
            Assert.Equal(cliente.Qtde, viewmodel.Quantidade);
            Assert.Equal(cliente.Nome + " - " + cliente.Sobrenome, viewmodel.NomeCompleto);
        }

        [Fact]
        public void MapperClienteToClienteViewModelList()
        {
            var clientes = new List<Cliente>()
            {
                new Cliente()
                {
                    ID = 1,
                    Ativo = true,
                    DataNasc = DateTime.Now.AddYears(-30),
                    Nome = "Ana",
                    Sobrenome = "Silva",
                    CodContrato = 1001,
                    Qtde = 22
                },
                new Cliente()
                {
                    ID = 2,
                    Ativo = true,
                    DataNasc = DateTime.Now.AddYears(-25),
                    Nome = "Bia",
                    Sobrenome = "Oliveira",
                    CodContrato = 2002,
                    Qtde = 10
                }
            };

            _mapper
                .SetConfig<Cliente, ClienteViewModel>(x => x.CodContrato, x => x.CodigoContrato)
                .SetConfig<Cliente, ClienteViewModel>(x => x.Qtde, x => x.Quantidade)
                .SetConfig<Cliente, ClienteViewModel>(x => x.Nome, x => x.NomeCompleto, x => x.Sobrenome, " - ");

            var viewmodel = _mapper.Map<Cliente, ClienteViewModel>(clientes).ToList();

            for (int i = 0; i < clientes.Count; i++)
            {
                Assert.Equal(clientes[i].ID, viewmodel[i].ID);
                Assert.Equal(clientes[i].Ativo, viewmodel[i].Ativo);
                Assert.Equal(clientes[i].DataNasc, viewmodel[i].DataNasc);
                Assert.Equal(clientes[i].Nome, viewmodel[i].Nome);
                Assert.Equal(clientes[i].Sobrenome, viewmodel[i].Sobrenome);
                Assert.Equal(clientes[i].CodContrato, viewmodel[i].CodigoContrato);
                Assert.Equal(clientes[i].Qtde, viewmodel[i].Quantidade);
                Assert.Equal(clientes[i].Nome + " - " + clientes[i].Sobrenome, viewmodel[i].NomeCompleto);
            }
        }

        [Fact]
        public void CreateObjectTest()
        {
            var cliente = _mapper.CreateEntity<Cliente>(new Form()
            {
                FormName = "Cliente",
                Fields = new List<Field>()
                {
                    new Field() { FieldName = "Nome", Value = "Ana" },
                    new Field() { FieldName = "Sobrenome", Value = "Maria" }
                }
            });

            Assert.Equal("Ana", cliente.Nome);
            Assert.Equal("Maria", cliente.Sobrenome);
        }

    }
}
