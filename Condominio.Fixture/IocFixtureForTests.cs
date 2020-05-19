using Autofac;
using Condominio.Repository;
using Condominio.Repository.Contracts;
using Condominio.Services;
using Condominio.Services.Contracts;
using System;

namespace Condominio.Fixtures
{
    public class IocFixtureForTests
    {
        public IContainer Container { get; private set; }
        
        public IocFixtureForTests()
        {
            ContainerBuilder ContainerBuilder = new ContainerBuilder();
            ContainerBuilder = Ioc.RegisterIocMySqlRepository(ContainerBuilder);
            ContainerBuilder = Ioc.RegisterIocServices(ContainerBuilder);
            Container = ContainerBuilder.Build();
        }
    }
}
