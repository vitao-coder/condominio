using Autofac;
using Condominio.Fixtures;
using Condominio.Model;
using Condominio.Repository.Contracts;
using Condominio.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Condominio.Services.Tests
{
    public class ApartamentoServicesTests : IClassFixture<IocFixtureForTests>
    {

        readonly IocFixtureForTests _iocFixture;

        IApartamentoServices _apartamentoServices;

        IMainContext _mainContext;

        public ApartamentoServicesTests(IocFixtureForTests iocFixture)
        {
            _iocFixture = iocFixture;            
            _apartamentoServices = iocFixture.Container.Resolve<IApartamentoServices>();
            _mainContext = iocFixture.Container.Resolve<IMainContext>();
        }


        [Fact]
        public void ListarTodosApartamentosTest1()
        {
            var listTodosAptos = _apartamentoServices.ListarTodosApartamentos();
            Assert.True((listTodosAptos.Count>1));
        }

        [Fact]
        public void AdicionarApartamentoTest2()
        {
            Apartamento apto = new Apartamento()
            {
                Bloco = new Bloco() { Descricao = "Bloco Service" },
                Numero = 992
            };

            var adicionarApartamento = _apartamentoServices.AdicionarApartamento(apto);
            Assert.True(adicionarApartamento);


            Apartamento aptoComMoradores = new Apartamento()
            {
                Bloco = new Bloco() { Descricao = "Bloco Service 2" },
                Numero = 993
            };

            List<Morador> moradores = new List<Morador>();            
            for (int i = 0; i < 10; i++)
            {
                var moradorAdd = new Morador()
                {
                    Cpf = 01988377129,
                    DataNascimento = Convert.ToDateTime("28/06/1985"),
                    Fone = "61993766328",
                    NomeCompleto = "Vitão " + i.ToString(),
                    Apartamento = aptoComMoradores
                };
                moradores.Add(moradorAdd);
            }

            var adicionarApartamentoComMoradores = _apartamentoServices.AdicionarApartamento(aptoComMoradores);
            Assert.True(adicionarApartamentoComMoradores);

            //Save Changes
            var saveChanges = _mainContext.SaveChangesAsync();
            Assert.True(saveChanges.IsCompletedSuccessfully);
        }

        [Fact]
        public void AlterarApartamentoTest3()
        {
            var listTodosAptos = _apartamentoServices.ListarTodosApartamentos();
            Assert.True((listTodosAptos.Count > 1));

            var aptoAlterar = listTodosAptos.FirstOrDefault();
            aptoAlterar.Numero = 997;

            var alterarApartamento = _apartamentoServices.AlterarApartamento(aptoAlterar);
            Assert.True(alterarApartamento);

            //Save Changes
            var saveChanges = _mainContext.SaveChangesAsync();
            Assert.True(saveChanges.IsCompletedSuccessfully);
        }

        [Fact]
        public void ExcluirApartamentoTest4()
        {
            var listTodosAptos = _apartamentoServices.ListarTodosApartamentos().Where(it => it.Numero == 992).ToList();
            Assert.True((listTodosAptos.Count >= 1));

            long idAptoExcluir = listTodosAptos.FirstOrDefault().Id;

            var excluirApartamento = _apartamentoServices.ExcluirApartamento(idAptoExcluir);
            Assert.True(excluirApartamento);

            var saveChanges = _mainContext.SaveChangesAsync();
            Assert.True(saveChanges.IsCompletedSuccessfully);
        }
    }
}
