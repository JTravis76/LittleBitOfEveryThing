using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace WebApp.Api.Extensions
{
    public static class ApplicationBuilderExt
    {
        public static void NoCacheHeader(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate, max-age=0");
                context.Response.Headers.Add("Pragma", "no-cache");
                context.Response.Headers.Add("Expires", "0");

                await next();
            });
        }

        public static void ExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(err =>
            {
                err.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogCritical("{p0} {p1} {p2}", contextFeature.Error.Message, contextFeature.Error.StackTrace, contextFeature.Error.InnerException?.Message);

                        await context.Response.WriteAsync(new
                        {
                            context.Response.StatusCode,
                            contextFeature.Error.Message,
                            Exception = contextFeature.Error.InnerException.Message
                        }.ToString());
                    }
                });
            });
        }

        public static void ExceptionHandlerWithEmail(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(err =>
            {
                err.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        System.Text.StringBuilder mailBody = new System.Text.StringBuilder();

                        mailBody.Append($"<h1>Error: {contextFeature.Error.Message}</h1>{contextFeature.Error.Source}<hr />{context.Request.Path}<br />");
                        mailBody.Append($"QueryString: {context.Request.QueryString}<hr />");
                        mailBody.Append($"Stack Trace<hr />{contextFeature.Error.StackTrace.Replace(System.Environment.NewLine, "<br />")}");

                        if (contextFeature.Error.InnerException != null)
                            mailBody.Append($"Inner Exception<hr />{contextFeature.Error.InnerException?.Message.Replace(System.Environment.NewLine, "<br />")}");

                        var email = context.Features.Get<Interfaces.IEmail>();
                        await email.SendEmail(new string[] { "jeremy.travis@ports.ppo.gov" }, "Netcenter Server Error", mailBody.ToString(), true, null);

                        logger.LogCritical("{p0} {p1}", contextFeature.Error.Message, contextFeature.Error.StackTrace);

                        await context.Response.WriteAsync(new
                        {
                            context.Response.StatusCode,
                            Message = contextFeature.Error
                        }.ToString());
                    }
                });
            });
        }

        /// <summary>
        /// Add Netcenter Roles to User's Identity
        /// </summary>
        /// <param name="app"></param>
        public static void UseNetcenterRoles(this IApplicationBuilder app)
        {
            //app.Use(async (context, next) =>
            //{
            //    if (context.User != null && context.User.Identity.IsAuthenticated)
            //    {
            //        var claims = new System.Collections.Generic.List<System.Security.Claims.Claim>();

            //        //== Might need fetch svc from app
            //        //var a = app.ApplicationServices.GetService<Netcenter.Sdk.INetcenterSDK>();

            //        var netcenter = context.RequestServices.GetService(typeof(Netcenter.Sdk.INetcenterSDK)) as Netcenter.Sdk.INetcenterSDK;
            //        netcenter.UserAppRoleNames(context.User.Identity.Name)
            //            .ForEach(delegate (string role)
            //            {
            //                claims.Add(new Claim(ClaimTypes.Role, role));
            //            });

            //        //== Doing this will require devs to merge identities in controller or services
            //        //var claimsIdentity = new System.Security.Claims.ClaimsIdentity(claims);
            //        //context.User.AddIdentity(claimsIdentity);

            //        //== Add new Role claims directly to default identity
            //        context.User.Identities.FirstOrDefault().AddClaims(claims);

            //        //await next(context);
            //    }

            //    await next();
            //});
        }

    }
}
