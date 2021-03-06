using System;
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
using DocumentLibrary.Infrastructure.Paging;
using DocumentLibrary.Infrastructure.Sorting;
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
            services.AddScoped<IPagingService, PagingService>();
            services.AddScoped<ISortingService, SortingService>();
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
            
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });
        }
    }
}