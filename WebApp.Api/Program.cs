using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApp.Api.Database;
using WebApp.Api.Extensions;
using WebApp.Api.Interfaces;
using WebApp.Api.Models;
using WebApp.Api.Services;
using WebApp.Extensions;
//using Ports.Thycotic.Provider;

namespace WebApp.Api
{
    public class Program
    {
        public Microsoft.Extensions.Logging.ILogger<Program>? Logger { get; }

        public Program() 
        {
            Logger = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("WebApp.Api.Program", LogLevel.Debug)
                    .AddConsole()
                    .AddDebug();
            }).CreateLogger<Program>();
        }


        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", false, true);
                //== A nice way to stored encrypted string within the AppSettings.json then decrypt before use.
                //config.AddEncryptedJson($"appsettings.json", false, false);
            })
            //.ConfigureAppConfiguration(ThycoticConfiguration.AddThycoticConfiguration)
            .ConfigureLogging(log =>
            {
                log.ClearProviders();

                log.AddConsole();
                log.AddDebug();
                //log.AddEventSourceLogger();

                //log.AddEventLog(new Microsoft.Extensions.Logging.EventLog.EventLogSettings
                //{
                //  LogName = "Application",
                //  SourceName = "WebApp"
                //});
            });

            //string connectionString = EnvironmentVariables.DbConnectionString.Get()
            //    ?? throw new ArgumentNullException("Unable to retrieve DB connection string");

            // Add services to the container.

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

            builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("Sqlite")));
            //builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(connectionString));

            builder.Services.AddScoped<IConfigurationSvc, ConfigurationSvc>();

            builder.Services.AddSingleton(svc =>
            {
                return new Models.UserModel
                {
                    Email = "jtravis28@gmail.com",
                    TwoFactorEnabled = false
                };
            });

            // !! use NewtonSoft for EnitityGraph
            builder.Services.AddControllers().AddNewtonsoftJson();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "WebApp.Api", Version = "v1" });
            });

            /*== Could pass other DI services into the GraphQL constructor */
            //ServiceProvider provider = services.BuildServiceProvider();
            //var someInterface = provider.GetRequiredService<WebApp.ISomeInterface>();
            builder.Services.AddSingleton(new Services.GqlSvc(builder.Configuration).MakeSchema());

            //builder.Services.AddRazorLightEngine();
            //builder.Services.AddScoped<Interfaces.IEmail, Services.EmailSvc>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(opt =>
                {
                    opt.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                });
            }

            //var webSocketOpts = new WebSocketOptions 
            //{
            //    KeepAliveInterval = TimeSpan.FromSeconds(120),
            //};
            //webSocketOpts.AllowedOrigins.Add("https://domain.net");
            //app.UseWebSockets(webSocketOpts);

            app.UseHttpsRedirection();
            app.UseFileServer();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            //== Use either one, both do the same functionally
            //app.UseMiddleware<NetcenterRoles>();
            app.UseNetcenterRoles();

            //app.ExceptionHandler(Logger);

            //app.UseMiddleware<WebSocketServer>();
            app.MapControllers();
            //app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.Run();
        }
    }
}