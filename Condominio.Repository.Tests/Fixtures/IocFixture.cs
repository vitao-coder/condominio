using Autofac;
using Condominio.Repository.Contracts;
using Condominio.Repository;


namespace Condominio.Repository.Tests.Fixtures
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
                })
                .InstancePerDependency().InstancePerLifetimeScope();//instance per dependecy and per lifetimescope to resolve injection in unit tests

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
