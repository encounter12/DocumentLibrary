using DocumentLibrary.Data.Context;
using DocumentLibrary.DTO;
using DocumentLibrary.Services;
using DocumentLibrary.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DocumentLibrary.DI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyInjectionBindings(
            this IServiceCollection services, AppData appData)
        {
            BindServices(services);
            BindDbContexts(services, appData);

            return services;
        }
        
        private static void BindServices(IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
        }
        
        private static void BindDbContexts(IServiceCollection services, AppData appData)
        {
            services.AddDbContext<DocumentLibraryContext>(options => 
                options.UseSqlServer(appData.DocumentLibraryConnectionString));
        }
    }
}