using System.Reflection;

namespace PIva.Api.Endpoints.Internal
{
    public static class EndpointExtensions
    {
        public static void AddServices<TMarker>(this IServiceCollection services, IConfiguration config)
        {
            AddServices(services, typeof(TMarker), config);
        }
        public static void AddServices(this IServiceCollection services, Type marker, IConfiguration config)
        {
            var types = GetTypesFromAssembly(marker);
            foreach (var type in types)
            {
                type.GetMethod(nameof(IEndpoints.AddServices))!.Invoke(null, new object[] { services, config });
            }
        }

        public static void UseEndpoints<TMarker>(this IEndpointRouteBuilder app)
        {
            UseEndpoints(app, typeof(TMarker));
        }
        public static void UseEndpoints(this IEndpointRouteBuilder app, Type marker)
        {
            var types = GetTypesFromAssembly(marker);
            foreach (var type in types)
            {
                type.GetMethod(nameof(IEndpoints.DefineEndpoints))!.Invoke(null, new object[] { app });
            }
        }

        private static IEnumerable<TypeInfo> GetTypesFromAssembly(Type type)
        {
            var types = type.Assembly.DefinedTypes
                        .Where(t => !t.IsAbstract && !t.IsInterface && typeof(IEndpoints).IsAssignableFrom(t));
            return types;
        }
    }
}
