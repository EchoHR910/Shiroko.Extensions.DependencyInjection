using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Shiroko.Extensions.DependencyInjection.PropertyInjection
{
    public static class ServiceCollectionExtension
    {
        public static void UsePropertyInjection(this IServiceCollection services)
        {
            services.TryAddTransient(typeof(IResolvedService<>), typeof(ServiceResolver<>));
        }
    }
}