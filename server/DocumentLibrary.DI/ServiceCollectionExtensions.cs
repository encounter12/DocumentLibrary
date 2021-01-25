using DocumentLibrary.Data.Context;
using DocumentLibrary.Data.Repositories;
using DocumentLibrary.Data.Repositories.Contracts;
using DocumentLibrary.DTO;
using DocumentLibrary.Infrastructure.AspNetHelpers;
using DocumentLibrary.Infrastructure.AspNetHelpers.Contracts;
using DocumentLibrary.Infrastructure.AspNetHelpers.UserService;
using DocumentLibrary.Infrastructure.DateTimeHelpers;
using DocumentLibrary.Services;
using DocumentLibrary.Services.Contracts;
using Microsoft.AspNetCore.Http;
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
            BindInfrastructureServices(services);
            BindDbContexts(services, appData);
            BindRepositories(services);
            BindAspNetHelpers(services);

            return services;
        }
        
        private static void BindServices(IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IGenreService, GenreService>();
        }

        private static void BindInfrastructureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDateTimeHelper, DateTimeHelper>();
            services.AddScoped<IPageFilterValidator, PageFilterValidator>();
        }
        
        private static void BindDbContexts(IServiceCollection services, AppData appData)
        {
            services.AddDbContext<DocumentLibraryContext>(options =>
                options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(appData.DocumentLibraryConnectionString));
        }
        
        private static void BindRepositories(IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
        }

        private static void BindAspNetHelpers(IServiceCollection services)
        {
            services.AddScoped<IModelStateErrorHandler, ModelStateErrorHandler>();
        }
    }
}