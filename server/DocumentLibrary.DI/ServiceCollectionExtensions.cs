using DocumentLibrary.Data.Context;
using DocumentLibrary.Data.Repositories;
using DocumentLibrary.Data.Repositories.Contracts;
using DocumentLibrary.DTO;
using DocumentLibrary.Services;
using DocumentLibrary.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DocumentLibrary.DI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyInjectionBindings(
            this IServiceCollection services, AppData appData)
        {
            BindServices(services);
            BindDbContexts(services, appData);
            BindRepositories(services);

            return services;
        }
        
        private static void BindServices(IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IGenreService, GenreService>();
        }
        
        private static void BindDbContexts(IServiceCollection services, AppData appData)
        {
            services.AddDbContext<DocumentLibraryContext>(options => 
                options.UseSqlServer(appData.DocumentLibraryConnectionString));
        }
        
        private static void BindRepositories(IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
        }
    }
}