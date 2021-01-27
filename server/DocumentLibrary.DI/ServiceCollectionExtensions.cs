using System.Text;
using DocumentLibrary.Data.Context;
using DocumentLibrary.Data.Identity;
using DocumentLibrary.Data.Repositories;
using DocumentLibrary.Data.Repositories.Contracts;
using DocumentLibrary.DTO.Config;
using DocumentLibrary.Infrastructure.AspNetHelpers;
using DocumentLibrary.Infrastructure.AspNetHelpers.Contracts;
using DocumentLibrary.Infrastructure.AspNetHelpers.UserService;
using DocumentLibrary.Infrastructure.DateTimeHelpers;
using DocumentLibrary.Services;
using DocumentLibrary.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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
            BindIdentity(services, appData);
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

        private static void BindIdentity(IServiceCollection services, AppData appData)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()  
                .AddEntityFrameworkStores<DocumentLibraryContext>()  
                .AddDefaultTokenProviders();  
  
            // Adding Authentication  
            services.AddAuthentication(options =>  
                {  
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;  
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;  
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;  
                })  
  
                // Adding Jwt Bearer  
                .AddJwtBearer(options =>  
                {  
                    options.SaveToken = true;  
                    options.RequireHttpsMetadata = false;  
                    options.TokenValidationParameters = new TokenValidationParameters()  
                    {  
                        ValidateIssuer = true,  
                        ValidateAudience = true,  
                        ValidAudience = appData.JwtConfig.ValidAudience,  
                        ValidIssuer = appData.JwtConfig.ValidIssuer,  
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(appData.JwtConfig.IssuerSigningKey))  
                    };  
                }); 
        }
    }
}