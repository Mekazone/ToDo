using Autofac;

namespace DoToo
{
    //The resolver is responsible for creating objects for us based on the type that we request.
    public static class Resolver
    {
        private static IContainer container;

        public static void Initialize(IContainer container)
        {
            Resolver.container = container;
        }

        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
}