using Autofac;
using Condominio.Fixtures;
using Condominio.Model;
using Condominio.Repository.Contracts;
using Condominio.Services.Contracts;
using Org.BouncyCastle.Crypto.Prng;
using System;
using System.Linq;
using Xunit;

namespace Condominio.Services.Tests
{
    public class MoradorServicesTests : IClassFixture<IocFixture>
    {
        readonly IocFixture _iocFixture;

        IMoradorServices _moradorServices;
        IApartamentoServices _apartamentoServices;
        IMainContext _mainContext;

        public MoradorServicesTests(IocFixture iocFixture)
        {
            _iocFixture = iocFixture;
            _moradorServices = iocFixture.Container.Resolve<IMoradorServices>();
            _mainContext = iocFixture.Container.Resolve<IMainContext>();
            _apartamentoServices = iocFixture.Container.Resolve<IApartamentoServices>();
        }

        [Fact]
        public void BuscarMoradoresPorApartamentoTest1()
        {
            var listTodosAptos = _apartamentoServices.ListarTodosApartamentos();
            Assert.True((listTodosAptos.Count > 1));

            var aptoBuscar = listTodosAptos.FirstOrDefault(it=>it.Moradores.Count>1);
            var aptoId = aptoBuscar.Id;

            var listMoradoresPorApartamento = _moradorServices.BuscarMoradoresPorApartamento(aptoId);
            Assert.True((listMoradoresPorApartamento.Count >= 1));
        }

        [Fact]
        public void BuscarMoradoresPorFiltroTest2()
        {
            string cpfPesquisa = "01988377129";
            string dataPesquisa = "28/06/1985";
            string nomeOuParte = "Vitão 0";

            var listMoradoresPorCpf = _moradorServices.BuscarMoradoresPorFiltro(MoradorServices.FiltroBuscaMorador.Cpf, cpfPesquisa);            
            var listMoradoresPorDataNascimento = _moradorServices.BuscarMoradoresPorFiltro(MoradorServices.FiltroBuscaMorador.DataNascimento, dataPesquisa);
            var listMoradoresPorNome = _moradorServices.BuscarMoradoresPorFiltro(MoradorServices.FiltroBuscaMorador.NomeCompletoOuParteDele, nomeOuParte);

            Assert.True((listMoradoresPorCpf.Count >= 1));
            Assert.True((listMoradoresPorDataNascimento.Count >= 1));
            Assert.True((listMoradoresPorNome.Count >= 1));
        }


        [Fact]
        public void AdicionarMoradorTest3()
        {
            var listTodosAptos = _apartamentoServices.ListarTodosApartamentos();
            Assert.True((listTodosAptos.Count > 1));
            var aptoNovoMorador = listTodosAptos.FirstOrDefault(it => it.Moradores.Count > 1);

            Morador novoMorador = new Morador()
            {
                Apartamento = aptoNovoMorador,
                Cpf = 123456789,
                DataNascimento = DateTime.Parse("28/04/2001"),
                Fone = "6133566565",
                NomeCompleto = "Jão do caminhãoo"
            };
            _mainContext.GetSet<Apartamento>().Attach(aptoNovoMorador);

            var adicionarMorador = _moradorServices.AdicionarMorador(novoMorador);
            Assert.True(adicionarMorador);

            //Save Changes
            var saveChanges = _mainContext.SaveChangesAsync();
            Assert.True(saveChanges.IsCompletedSuccessfully);
        }

        [Fact]
        public void AlterarMoradorTest4()
        {
            var listTodosAptos = _apartamentoServices.ListarTodosApartamentos();
            Assert.True((listTodosAptos.Count > 1));
            var aptoComMoradores = listTodosAptos.FirstOrDefault(it => it.Moradores.Count > 1);
            var moradorAlterar = aptoComMoradores.Moradores.FirstOrDefault();

            var aptoAlterarComMoradores = listTodosAptos.LastOrDefault(it => it.Moradores.Count > 1);

            moradorAlterar.NomeCompleto = "Meu nome Novo apto id " + aptoAlterarComMoradores.Id;
            moradorAlterar.Apartamento = aptoAlterarComMoradores;

            _mainContext.GetSet<Apartamento>().Attach(aptoAlterarComMoradores);

            var alterarMorador = _moradorServices.AlterarMorador(moradorAlterar);
            Assert.True(alterarMorador);

            //Save Changes
            var saveChanges = _mainContext.SaveChangesAsync();
            Assert.True(saveChanges.IsCompletedSuccessfully);
        }
        
        [Fact]
        public void ExcluirMoradorTest5()
        {
            var moradorExcluir = _moradorServices.BuscarMoradoresPorFiltro(MoradorServices.FiltroBuscaMorador.Cpf, "123456789").FirstOrDefault();

            var excluirMorador = _moradorServices.ExcluirMorador(moradorExcluir.Id);
            Assert.True(excluirMorador);

            //Save Changes
            var saveChanges = _mainContext.SaveChangesAsync();
            Assert.True(saveChanges.IsCompletedSuccessfully);
        }

    }
}
