using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using WebAppFunction.Api.Classes;
using WebAppFunction.Api.Converters;
using WebAppFunction.Api.Database;
using WebAppFunction.Api.Entities;
using WebAppFunction.Api.Services;
using WebAppFunction.Api.Services.Interfaces;

[assembly: WebJobsStartup(typeof(WebAppFunction.Api.Startup))]

namespace WebAppFunction.Api
{
    public class Startup : IWebJobsStartup
  {
    public void Configure(IWebJobsBuilder builder)
    {
      ConfigureDependencies(builder.Services);
    }

    private static void ConfigureDependencies(IServiceCollection services)
    {
      var dbConnString = EnvironmentVariables.DbConnectionString.Get()
                         ?? throw new Exception("Unable to retrieve DB connection string");
      services
          .AddDbContext<ROWContext>(options => options.UseSqlServer(dbConnString))
          .AddScoped<IConfigurationService, ConfigurationService>()
          .AddScoped<IAdministrationService, AdministrationService>()
          .AddScoped<IGraphService, GraphService>()
          .AddScoped<IEmailService, EmailService>()
          .Configure<JsonSerializerOptions>(opt =>
          {
            opt.PropertyNameCaseInsensitive = true;
            opt.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
          });

      ConfigureAutoMapper(services);
      ConfigureLogging(services);
    }

    private static void ConfigureAutoMapper(IServiceCollection services)
    {
      // this will add all classes inheriting Profile to the configuration for AutoMapper
      services.AddAutoMapper(config =>
          config.AddMaps(typeof(Startup)));

      services.AddTransient<AuthModelConverter>();
      services.AddTransient<DynamicConfigurationModelConverter>();
      services.AddTransient<GenericRequestConverter<GrantorType>>();
    }

    private static void ConfigureLogging(IServiceCollection services)
    {
      var logLevel = LogLevel.Information;
#if DEBUG
      logLevel = LogLevel.Trace;
#endif
      services.AddLogging(loggingBuilder => loggingBuilder.AddFilter("", logLevel));
    }
  }
}
