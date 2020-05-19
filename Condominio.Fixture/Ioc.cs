using Autofac;
using Condominio.Repository;
using Condominio.Repository.Contracts;
using Condominio.Services;
using Condominio.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Condominio.Fixtures
{
    public static class Ioc
    {

        public static ContainerBuilder RegisterIocMySqlRepository(ContainerBuilder builder)
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

        public static ContainerBuilder RegisterIocServices(ContainerBuilder builder)
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
