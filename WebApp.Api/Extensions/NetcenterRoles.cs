//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using Microsoft.Extensions.DependencyInjection;

//namespace WebApp.Api.Extensions
//{
//    public class NetcenterRoles
//    {
//        private readonly Microsoft.AspNetCore.Http.RequestDelegate _next;

//        /// <summary>
//        /// Middleware to add Netcenter Roles to User's Identity
//        /// </summary>
//        /// <param name="next"></param>
//        public NetcenterRoles(Microsoft.AspNetCore.Http.RequestDelegate next)
//        {
//            _next = next;
//        }

//        public async System.Threading.Tasks.Task InvokeAsync(Microsoft.AspNetCore.Http.HttpContext httpContext)
//        {
//            if (httpContext.User != null && httpContext.User.Identity.IsAuthenticated)
//            {
//                var claims = new System.Collections.Generic.List<System.Security.Claims.Claim>();

//                var netcenter = httpContext.RequestServices.GetService<Netcenter.Sdk.INetcenterSDK>();
//                netcenter.UserAppRoleNames(httpContext.User.Identity.Name)
//                    .ForEach(delegate (string role) {
//                        claims.Add(new Claim(ClaimTypes.Role, role));
//                    });

//                // Doing this will require devs to merge identities in controller or services
//                //var claimsIdentity = new System.Security.Claims.ClaimsIdentity(claims);
//                //httpContext.User.AddIdentity(claimsIdentity);

//                //add new Role claims directly to default identity
//                httpContext.User.Identities.FirstOrDefault().AddClaims(claims);

//                await _next(httpContext);
//            }
//        }
//    }
//}
