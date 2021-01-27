using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using DocumentLibrary.DI;
using DocumentLibrary.DTO;
using DocumentLibrary.DTO.Config;

namespace DocumentLibrary.API.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var jwtBearer = new JwtConfig();
            Configuration.GetSection(nameof(JwtConfig)).Bind(jwtBearer);
            
            var appData = new AppData()
            {
                JwtConfig = jwtBearer,
                DocumentLibraryConnectionString = Configuration.GetConnectionString("DocumentLibraryConnection")
            };
            
            services.AddDependencyInjectionBindings(appData);
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            services.AddDatabaseDeveloperPageExceptionFilter();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "DocumentLibrary.API.Admin", Version = "v1"});
            });
        }
        
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => 
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DocumentLibrary.API.Admin v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}