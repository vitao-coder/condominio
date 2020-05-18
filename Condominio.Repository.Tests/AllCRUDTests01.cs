using Xunit;
using Condominio.Repository.Tests.Fixtures;
using Condominio.Repository.Contracts;
using Condominio.Model;
using Autofac;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            var blocoAdd = new Bloco() 
            { Descricao = "Bloco A" };            
            var addBloco = _blocoRepository.AddAsync(blocoAdd);
            Assert.True(addBloco.IsCompletedSuccessfully);

            //Add Apartamento
            var apartamentoAdd= new Apartamento() 
            {Bloco = blocoAdd, Numero = 1,Moradores = new List<Morador>()};
            var addApartamento = _apartamentoRepository.AddAsync(apartamentoAdd);
            Assert.True(addApartamento.IsCompletedSuccessfully);

            //Add Moradores
            var moradoresAdd = new List<Morador>();
            for (int i = 0; i < 10; i++)
            {
                var moradorAdd = new Morador()
                { Cpf = 01988377129, DataNascimento = Convert.ToDateTime("28/06/1985"), 
                    Fone = "61993766328", NomeCompleto = "Vitão " + i.ToString(),
                Apartamento = apartamentoAdd };
                moradoresAdd.Add(moradorAdd);
            }
            var addMoradores = _moradorRepository.AddAsync(moradoresAdd);
            Assert.True(addApartamento.IsCompletedSuccessfully);

            //Save Changes
            var saveChanges = _mainContext.SaveChangesAsync();
            Assert.True(saveChanges.IsCompletedSuccessfully);
        }


        [Fact]
        public void T2Update()
        {
            //Upd Bloco
            string descricaoUpd = "Updated";
            var blocoUpd = _blocoRepository.GetAsync(bl => bl.Descricao.Contains("Bloco")).Result.FirstOrDefault();
            blocoUpd.Descricao = descricaoUpd;
            long idBlocoUpd = blocoUpd.Id;            
            var updBloco = _blocoRepository.UpdateAsync(blocoUpd);
            Assert.True(updBloco.IsCompletedSuccessfully);            

            //Upd Apartamento
            int numeroUpd = 999;
            var queryApto = _apartamentoRepository.GetQueryable(apt => apt.Moradores.Count > 1).Include(apt => apt.Moradores);
            var aptoUpd = queryApto.FirstOrDefault();
            aptoUpd.Numero = numeroUpd;
            aptoUpd.Bloco = blocoUpd;
            long idAptoUpd = aptoUpd.Id;
            var updApto = _apartamentoRepository.UpdateAsync(aptoUpd);
            Assert.True(updApto.IsCompletedSuccessfully);

            //Upd Morador            
            string nomeUpd = "Vitão Updated";
            var moradorUpd = aptoUpd.Moradores.FirstOrDefault();
            moradorUpd.NomeCompleto = nomeUpd;

            //Save Changes
            var saveChanges = _mainContext.SaveChangesAsync();
            Assert.True(saveChanges.IsCompletedSuccessfully);

            //test bloco updated data
            var blocoUpdated = _blocoRepository.GetAsync(idBlocoUpd).Result;
            Assert.Equal(descricaoUpd, blocoUpdated.Descricao);

            //test apto updated data
            var aptoUpdated = _apartamentoRepository.GetAsync(idAptoUpd).Result;
            Assert.Equal(numeroUpd, aptoUpdated.Numero);
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

