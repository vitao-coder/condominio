using Autofac;
using Condominio.Repository;
using Condominio.Repository.Contracts;
using Condominio.Services;
using Condominio.Services.Contracts;
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
            builder = RegisterIocServices(builder);
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

        private ContainerBuilder RegisterIocServices(ContainerBuilder builder)
        {
            builder.RegisterType<MoradorServices>().As<IMoradorServices>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ApartamentoServices>().As<IApartamentoServices>()
                .InstancePerLifetimeScope();

            builder.RegisterType<BlocoServices>().As<IBlocoServices>()
                .InstancePerLifetimeScope();

            return builder;
        }

    }
}
