using Autofac;
using Condominio.Repository;
using Condominio.Repository.Contracts;
using System;

namespace Condominio.Fixtures
{
    public class IocFixture
    {
        public IContainer Container { get; private set; }

        public IocFixture()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder = RegisterIocMySqlRepository(builder);
            Container = builder.Build();
        }

        private ContainerBuilder RegisterIocMySqlRepository(ContainerBuilder builder)
        {
            builder.RegisterType<MainContext>().As<IMainContext>()
                .WithParameters(
                new[]{
                    new NamedParameter("connectionString","Server=localhost; Database=condominiodb; Uid=root; Pwd=senha;")
                }).InstancePerLifetimeScope();

            //
            builder.RegisterGeneric(typeof(BaseRepository<>))
              .As(typeof(IBaseRepository<>))
              .InstancePerDependency();

            var assemblyType = typeof(IBaseRepository<>).Assembly;

            builder.RegisterAssemblyTypes(assemblyType)
                .AsClosedTypesOf(typeof(IBaseRepository<>));

            return builder;
        }

    }
}
