using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using XMEN.Core.Interfaces;
using XMEN.Core.Services;
using XMEN.Infrastructure.Configurations.Data;

namespace XMEN.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MutantContext>(
                options => options.UseNpgsql(
                    configuration.GetConnectionString("XMenDB"),
                    NpgsqlOptions => NpgsqlOptions.MigrationsAssembly("XMEN.Api")
                    )
            );
            return services;
        }


        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IMutantService, MutantService>();

            //services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            //services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }


        public static IServiceCollection AddSwagger(this IServiceCollection services, string xmlFileName)
        {
            services.AddSwaggerGen(doc =>
            {
                doc.SwaggerDoc("v1", new OpenApiInfo { Title = "X-MEN API", Version = "v1" });
                //Api authorization is controlled by aws api gateway

                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
                doc.IncludeXmlComments(xmlPath);
            });

            return services;
        }

    }
}

