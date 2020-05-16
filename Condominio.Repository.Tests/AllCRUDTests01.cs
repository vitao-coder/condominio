using Xunit;
using Condominio.Repository.Tests.Fixtures;
using Condominio.Repository.Contracts;
using Condominio.Model;
using Autofac;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Condominio.Repository.Tests
{
    public class AllCRUDTests01 : IClassFixture<IocFixture>
    {
        readonly IocFixture _iocFixture;

        IBaseRepository<Apartamento> _apartamentoRepository;
        IBaseRepository<Bloco> _blocoRepository;
        IBaseRepository<Morador> _moradorRepository;
        IMainContext _mainContext;

        public AllCRUDTests01(IocFixture iocFixture)
        {
            _iocFixture = iocFixture;
            _mainContext = iocFixture.Container.Resolve<IMainContext>();
            _apartamentoRepository = iocFixture.Container.Resolve<IBaseRepository<Apartamento>>();
            _blocoRepository = iocFixture.Container.Resolve<IBaseRepository<Bloco>>();
            _moradorRepository = iocFixture.Container.Resolve<IBaseRepository<Morador>>();            
        }

        [Fact]
        public void T1Add()
        {
            //Add Bloco
            var BlocoAdd = new Bloco() 
            { Descricao = "Bloco A" };            
            var addBloco = _blocoRepository.AddAsync(BlocoAdd);
            Assert.True(addBloco.IsCompletedSuccessfully);

            //Add Apartamento
            var Apartamento = new Apartamento() 
            {Bloco = BlocoAdd, Numero = 1,Moradores = new List<Morador>()};
            var addApartamento = _apartamentoRepository.AddAsync(Apartamento);
            Assert.True(addApartamento.IsCompletedSuccessfully);

            //Add Moradores
            List<Morador> Moradores = new List<Morador>();
            for (int i = 0; i < 10; i++)
            {
                var MoradorAdd = new Morador()
                { Cpf = 01988377129, DataNascimento = Convert.ToDateTime("28/06/1985"), Fone = "61993766328", NomeCompleto = "Vitão " + i.ToString() };
                Moradores.Add(MoradorAdd);
            }
            var addMoradores = _moradorRepository.AddAsync(Moradores);
            Assert.True(addApartamento.IsCompletedSuccessfully);


            //Save Changes
            var saveChanges = _mainContext.SaveChangesAsync();
            Assert.True(saveChanges.IsCompletedSuccessfully);
        }


        [Fact]
        public long T2Update()
        {
            long returnLong = 1;
            return returnLong;
        }

        [Fact]
        public long T3Get()
        {
            long returnLong = 1;
            return returnLong;
        }
        public long T4Delete()
        {
            long returnLong = 1;
            return returnLong;
        }
    }
}

