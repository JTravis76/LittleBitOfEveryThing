//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Net.Http;
//using System.Net.Mail;
//using Netcenter.Sdk;
//using WebApp.Infrastructure.Services.Repositories;
//using Microsoft.AspNetCore.Mvc;

//namespace WebApp.Api.Extensions
//{
//    public static class ServiceCollectionExt
//    {
//        public static void AddNetcenter(this IServiceCollection services, IConfiguration configuration)
//        {
//            services.AddSingleton(svc => new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true }));
//            services.AddSingleton(svc => NetcenterSDKFactory.Create(configuration["AppSettings:NetcenterApiUrl"],
//                configuration["AppSettings:NetcenterAppName"],
//                svc.GetService<HttpClient>()
//            ));

//            services.AddTransient<IAuthorizationPolicyProvider, Netcenter.AuthorizeFilter.NetcenterPolicyProvider>();
//            services.AddScoped<IAuthorizationHandler, Netcenter.AuthorizeFilter.AuthorizeHandler>();
//        }

//        public static void AddDataContext(this IServiceCollection services, IConfiguration configuration)
//        {
//            //services.AddDbContext<DataContext>(opt => opt.UseSqlite(configuration["ConnectionStrings:Sqlite"]));
//            services.AddDbContext<DataContext>( opt => opt.UseSqlServer(configuration["ConnectionStrings:WebApp"]));
//        }

//        public static void AddMvcSetup(this IServiceCollection services)
//        {
//            services.AddMvc(config =>
//            {
//                config.MaxModelValidationErrors = 10;
//            })
//            .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1)
//            .AddJsonOptions(opt =>
//            {
//                ////switch back to PascalCase from default camelCase
//                //opt.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
//                //opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
//                //opt.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
//            });

//            services.Configure<ApiBehaviorOptions>(opt => {
//                //Disables the automatic vaildation binding. Did this because I wasn't able to customize the returned result object
//                opt.SuppressModelStateInvalidFilter = true;

//                //opt.InvalidModelStateResponseFactory = actionContext =>
//                //    new Microsoft.AspNetCore.Mvc.BadRequestObjectResult(actionContext.ModelState);
//            });
//        }

//        public static void AddCorsSetup(this IServiceCollection services, string policyName = "CorsPolicy")
//        {
//            string[] origins = configuration["AllowOrigins"].Split(";");

//            services.AddCors(opt => {
//                opt.AddPolicy(policyName, builder => builder
//                    .AllowAnyOrigin()
//                    //.WithOrigins(origins)
//                    .AllowAnyMethod()
//                    .AllowAnyHeader()
//                    .AllowCredentials()); //<- !!MUST USE with Windows Authentication !!
//            });
//        }

//        public static void AddSmtpClient(this IServiceCollection services, IConfiguration configuration)
//        {
//            services.AddScoped(serviceProvider =>
//            {
//                return new SmtpClient()
//                {
//                    Host = configuration["Smtp:Host"],
//                    Port = Convert.ToInt32(configuration["Smtp:Port"]),
//                    EnableSsl = false,
//                    DeliveryMethod = SmtpDeliveryMethod.Network,
//                    UseDefaultCredentials = true
//                };
//            });
//        }

//        public static void AddEmail(this IServiceCollection services, IConfiguration configuration)
//        {
//            var provider = services.BuildServiceProvider();
//            var smtpClient = provider.GetRequiredService<SmtpClient>();

//            var emailSvc = new Infrastructure.Services.Email.EmailSvc(smtpClient, configuration["Smtp:From"]);
//            //bool test = emailSvc.SendEmail(new string[] { "TEST@domain.com" }, "Subject", "Body", false, null).Result;
//            services.AddSingleton(emailSvc);
//        }

//        public static void AddSwagger(this IServiceCollection services)
//        {
//            services.AddSwaggerGen(x => {
//                string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

//                x.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
//                {
//                    Title = "WebApp1 API",
//                    Description = "Internal facing API for WebApp1",
//                    Version = $"v{version}",
//                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact
//                    {
//                        Email = "FbpUserSupport@ports.pppo.gov",
//                        Name = "Contact Fluor-BWXT IT"
//                    }
//                });

//                // Set the comments path for the Swagger JSON and UI.
//                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
//                var xmlPath = System.IO.Path.Combine(System.AppContext.BaseDirectory, xmlFile);
//                x.IncludeXmlComments(xmlPath);
//            });
//        }

//        public static void AddRazorLightEngine(this IServiceCollection services)
//        {
//            services.AddScoped<RazorLight.IRazorLightEngine>(sp =>
//            {
//                var engine = new RazorLight.RazorLightEngineBuilder()
//                    .UseFilesystemProject(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location))
//                    .UseMemoryCachingProvider()
//                    .Build();

//                return engine;
//            });
//        }

//public static void AuthorizationPolices(this IServiceCollection services)
//{
//    services.AddAuthorization(opt =>
//    {
//        opt.AddPolicy("Netcenter",
//            policy =>
//            {
//                policy.AddRequirements(new Netcenter.AuthorizeFilter.AuthorizeRequirement(new string[] { "Admin" }));
//            });
//    });
//}

//public static void DefaultIdentity(this IServiceCollection services)
//{
//    /* Not sure how this works, something to do with built-in Razor pages
//     * When using Role-based authorization with MVC Razor templates 
//     */
//    services.AddDefaultIdentity<Microsoft.AspNetCore.Identity.IdentityUser>()
//        .AddRoles<Microsoft.AspNetCore.Identity.IdentityRole>();
//}
//    }
//}

//using System.Net.Mail;

//public static void AddIISIntegration(this IServiceCollection services)
//{
//    services.Configure<IISOptions>(opt => { });
//}

//public static void AddSmtpClient(this IServiceCollection services, IConfiguration configuration)
//{
//    services.AddScoped(serviceProvider =>
//    {
//        return new SmtpClient()
//        {
//            Host = configuration["Smtp:Host"],
//            Port = Convert.ToInt32(configuration["Smtp:Port"]),
//            EnableSsl = false,
//            DeliveryMethod = SmtpDeliveryMethod.Network,
//            UseDefaultCredentials = true
//        };
//    });


//public static void AddGraphQL(this IServiceCollection services, IConfiguration configuration)
//{
//    /*== Could pass other DI services into the GraphQL constructor */
//    //ServiceProvider provider = services.BuildServiceProvider();
//    //var netcenter = provider.GetRequiredService<Netcenter.Sdk.INetcenterSDK>();
//    //var emwaste = provider.GetRequiredService<EMWaste.Infrastructure.Interfaces.IEMWaste>();

//    var graphQL = new Services.GraphQL.GQLSvc(configuration);
//    services.AddSingleton(graphQL.MakeSchema());
//}