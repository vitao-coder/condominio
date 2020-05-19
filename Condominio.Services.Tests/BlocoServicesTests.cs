using Autofac;
using Condominio.Fixtures;
using Condominio.Repository.Contracts;
using Condominio.Services.Contracts;
using Xunit;

namespace Condominio.Services.Tests
{
    public class BlocoServicesTests : IClassFixture<IocFixture>
    {
        readonly IocFixture _iocFixture;

        IBlocoServices _blocoServices;
        IMainContext _mainContext;

        public BlocoServicesTests(IocFixture iocFixture)
        {
            _blocoServices = iocFixture.Container.Resolve<IBlocoServices>();
            _mainContext = iocFixture.Container.Resolve<IMainContext>();
        }

        [Fact]
        public void ListarTodosBlocosTest1()
        {
            var listTodosBlocos = _blocoServices.ListarTodosBlocos();
            Assert.True((listTodosBlocos.Count > 1));
        }
    }
}
