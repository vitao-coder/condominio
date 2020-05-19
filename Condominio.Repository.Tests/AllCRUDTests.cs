using Xunit;
using Condominio.Repository.Contracts;
using Condominio.Model;
using Autofac;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Condominio.Fixtures;

namespace Condominio.Repository.Tests
{
    public class AllCRUDTests : IClassFixture<IocFixtureForTests>
    {
        readonly IocFixtureForTests _iocFixture;

        IBaseRepository<Apartamento> _apartamentoRepository;
        IBaseRepository<Bloco> _blocoRepository;
        IBaseRepository<Morador> _moradorRepository;
        IMainContext _mainContext;

        public AllCRUDTests(IocFixtureForTests iocFixture)
        {              
            _iocFixture = iocFixture;            
            _mainContext = _iocFixture.Container.Resolve<IMainContext>();
            _apartamentoRepository = _iocFixture.Container.Resolve<IBaseRepository<Apartamento>>();
            _blocoRepository = _iocFixture.Container.Resolve<IBaseRepository<Bloco>>();
            _moradorRepository = _iocFixture.Container.Resolve<IBaseRepository<Morador>>();            
        }

        [Fact]
        public void T1Add()
        {
            //Add Bloco
            var blocoAdd = new Bloco() 
            { Descricao = "Bloco A" };            
            var addBloco = _blocoRepository.AddAsync(blocoAdd);
            Assert.True(addBloco.IsCompletedSuccessfully);

            //Add Bloco
            var blocoAdd2 = new Bloco()
            { Descricao = "Bloco B" };
            var addBloco2 = _blocoRepository.AddAsync(blocoAdd2);
            Assert.True(addBloco2.IsCompletedSuccessfully);

            //Add Apartamento
            var apartamentoAdd= new Apartamento() 
            {Bloco = blocoAdd, Numero = 1,Moradores = new List<Morador>()};
            var addApartamento = _apartamentoRepository.AddAsync(apartamentoAdd);
            Assert.True(addApartamento.IsCompletedSuccessfully);

            //Add Apartamento
            var apartamentoAdd2 = new Apartamento()
            { Bloco = blocoAdd2, Numero = 125, Moradores = new List<Morador>() };
            var addApartamento2 = _apartamentoRepository.AddAsync(apartamentoAdd);
            Assert.True(addApartamento2.IsCompletedSuccessfully);

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
            var updMorador = _moradorRepository.UpdateAsync(moradorUpd);
            Assert.True(updMorador.IsCompletedSuccessfully);
            long idMoradorUpd = moradorUpd.Id;

            //Upd moradores
            var rangeMoradores = aptoUpd.Moradores.Where(it => it.Id != moradorUpd.Id);
            string nomeUpdRange = "Vitão Updated Range";
            int contUpd = 0;
            var rangeUpdate = new List<Morador>();
            foreach (var morador in rangeMoradores)
            {
                morador.NomeCompleto = nomeUpdRange + " "+ contUpd.ToString();
                rangeUpdate.Add(morador);
                contUpd++;
            }
            var updMoradores = _moradorRepository.UpdateAsync(rangeUpdate);
            Assert.True(updMoradores.IsCompletedSuccessfully);

            //Save Changes
            var saveChanges = _mainContext.SaveChangesAsync();
            Assert.True(saveChanges.IsCompletedSuccessfully);

            //test bloco updated data
            var blocoUpdated = _blocoRepository.GetAsync(idBlocoUpd).Result;
            Assert.Equal(descricaoUpd, blocoUpdated.Descricao);

            //test apto updated data
            var aptoUpdated = _apartamentoRepository.GetAsync(idAptoUpd).Result;
            Assert.Equal(numeroUpd, aptoUpdated.Numero);

            //test morador updated data
            var moradorUpdated = _moradorRepository.GetAsync(idMoradorUpd).Result;
            Assert.Equal(nomeUpd, moradorUpd.NomeCompleto);
        }

        [Fact]
        public void T3Get()
        {
            var getAllBlocos = _blocoRepository.GetAsync().Result;
            int contBlocos = getAllBlocos.Count();
            Assert.True((contBlocos > 1));

            var getAllAptos = _apartamentoRepository.GetAsync().Result;
            int contAptos = getAllAptos.Count();
            Assert.True((contAptos > 1));

            var getAllMoradores = _moradorRepository.GetAsync().Result;
            int contMoradores = getAllMoradores.Count();
            Assert.True((contMoradores > 1));
        }

        [Fact]
        public void T4Delete()
        {
            //morador delete            
            var moradoresDelete = _moradorRepository
                .GetQueryable(it => it.NomeCompleto.Contains("Vitão"))                
                .Take(10).ToList();            
            var moradorDeleteWhere = _moradorRepository.DeleteAsync(moradoresDelete);
            Assert.Equal(Task.CompletedTask, moradorDeleteWhere);

            //apto delete
            var aptoDeleteEntity = _apartamentoRepository.GetAsync(it=>it.Numero==125).Result;            
            var delAptoEntity = _apartamentoRepository.DeleteAsync(aptoDeleteEntity);
            Assert.Equal(Task.CompletedTask, delAptoEntity);

            //bloco delete
            var blocoDeleteEntity = _blocoRepository.GetAsync(it => it.Descricao == "Bloco B").Result.FirstOrDefault();
            var delBlocoById = _blocoRepository.DeleteAsync(blocoDeleteEntity.Id);
            Assert.Equal(Task.CompletedTask, delBlocoById);

            //Save Changes
            var saveChanges = _mainContext.SaveChangesAsync();
            Assert.True(saveChanges.IsCompletedSuccessfully);
        }
    }
}

