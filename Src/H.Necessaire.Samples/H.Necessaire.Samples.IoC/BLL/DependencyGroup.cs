namespace H.Necessaire.Samples.IoC.BLL
{
    internal class DependencyGroup : ImADependencyGroup
    {
        public void RegisterDependencies(ImADependencyRegistry dependencyRegistry)
        {
            dependencyRegistry.Register<ImADataGenerator>(() => new Concrete.StaticDataGenerator());
        }
    }
}
