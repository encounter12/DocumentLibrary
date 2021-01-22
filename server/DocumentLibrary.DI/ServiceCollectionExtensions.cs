using DocumentLibrary.Services;
using DocumentLibrary.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace DocumentLibrary.DI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyInjectionBindings(this IServiceCollection services)
        {
            BindServices(services);

            return services;
        }
        
        private static void BindServices(IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
        }
    }
}