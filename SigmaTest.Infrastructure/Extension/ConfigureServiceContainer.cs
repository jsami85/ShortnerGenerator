using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SigmaTest.DataAccess;
using SigmaTest.Service.Contract;
using SigmaTest.Service.Implementation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace SigmaTest.Infrastructure.Extension
{
    public static class ConfigureServiceContainer
    {
        public static void AddDbContext(this IServiceCollection serviceCollection,
             IConfiguration configuration, IConfigurationRoot configRoot)
        {
            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
                   options.UseSqlServer(configuration.GetConnectionString("OnionArchConn") ?? configRoot["ConnectionStrings:OnionArchConn"]
                , b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        }

        public static void AddScopedServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

        }
        public static void AddTransientServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUrlManagerService, UrlManagerService>();
        }

        public static void AddSwaggerOpenAPI(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSwaggerGen(setupAction =>
            {

                setupAction.SwaggerDoc(
                    "OpenAPISpecification",
                    new OpenApiInfo()
                    {
                        Title = "Shortner Generator",
                        Version = "1",
                        Description = "Link Shortner Generator",
                        Contact = new OpenApiContact()
                        {
                            Email = "jsami99@gmail.com",
                            Name = "Mojy Sami"
                        },
                        License = new OpenApiLicense()
                        {
                            Name = "Test License"
                        }
                    });

               
            });

        }

        //public static void AddMailSetting(this IServiceCollection serviceCollection,
        //    IConfiguration configuration)
        //{
        //    serviceCollection.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        //}

        public static void AddController(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddControllers().AddNewtonsoftJson();
        }

        //public static void AddMediatorCQRS(this IServiceCollection services)
        //{
        //    //var assembly = AppDomain.CurrentDomain.Lsigmatestd("SigmaTest.Service");
        //    services.AddMediatR(Assembly.GetExecutingAssembly());
        //}
        public static void AddVersion(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }
        //public static void AddHealthCheck(this IServiceCollection serviceCollection, IConfiguration configuration)
        //{
        //    serviceCollection.AddHealthChecks()
        //         //.AddDbContextCheck<ApplicationDbContext>(name: "Application DB Context", failureStatus: HealthStatus.Degraded)
        //         .AddUrlGroup(new Uri("https://amitpnk.github.io/"), name: "My personal website", failureStatus: HealthStatus.Degraded)
        //         .AddSqlServer(configuration.GetConnectionString("OnionArchConn"));
        //    //.AddSqlServer(configuration.GetConnectionString("IdentityConnection"));

        //    serviceCollection.AddHealthChecksUI(setupSettings: setup =>
        //    {
        //        setup.AddHealthCheckEndpoint("Basic Health Check", $"http://localhost:44356/healthz");
        //    });
        //}

    }
}
