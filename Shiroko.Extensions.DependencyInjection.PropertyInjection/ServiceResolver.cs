using System.Collections.Concurrent;
using System.Reflection;

namespace Shiroko.Extensions.DependencyInjection.PropertyInjection
{
    /// <summary>
    /// Resolves a service and injects its properties marked with the <see cref="InjectAttribute"/>.
    /// </summary>
    /// <typeparam name="TService">The type of the service to resolve.</typeparam>
    /// <remarks>
    /// This class is used to resolve a service from the dependency injection container and inject its properties that are marked with the <see cref="InjectAttribute"/>.
    /// </remarks>
    internal class ServiceResolver<TService> : IResolvedService<TService> where TService : class
    {
        private static readonly ConcurrentDictionary<Type, PropertyInfo[]> _propertyCache = new();

        public ServiceResolver(IServiceProvider serviceProvider)
        {
            Service = ResolveService(serviceProvider);
        }

        public TService Service { get; }

        private static void InjectProperties(IServiceProvider services, object instance)
        {
            ArgumentNullException.ThrowIfNull(instance);

            var properties = _propertyCache.GetOrAdd(instance.GetType(), type =>
                type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(prop => Attribute.IsDefined(prop, typeof(InjectAttribute)) && prop.CanWrite)
                    .ToArray());

            foreach (var property in properties)
            {
                var dependency = services.GetService(property.PropertyType)
                    ?? throw new ServiceResolutionException($"Failed to resolve dependency for property '{property.Name}' of type '{property.PropertyType.FullName}'.");
                property.SetValue(instance, dependency);
            }
        }

        private static TService ResolveService(IServiceProvider services)
        {
            var service = services.GetService(typeof(TService));

            if (service is not TService typedService)
            {
                throw new ServiceResolutionException($"Unable to resolve service of type '{typeof(TService).FullName}'.");
            }

            InjectProperties(services, typedService);
            return typedService;
        }
    }
}