namespace H.Necessaire.Samples.IoC.BLL
{
    internal class DependencyGroup : ImADependencyGroup
    {
        public void RegisterDependencies(ImADependencyRegistry dependencyRegistry)
        {
            dependencyRegistry.Register<Concrete.StaticDataGenerator>(() => new Concrete.StaticDataGenerator());
            dependencyRegistry.Register<Concrete.RandomDataGenerator>(() => new Concrete.RandomDataGenerator());
            dependencyRegistry.Register<Concrete.ComposedDataGenerator>(() => new Concrete.ComposedDataGenerator());

            dependencyRegistry.Register<ImADataGenerator>(() => dependencyRegistry.Get<Concrete.ComposedDataGenerator>());
        }
    }
}
