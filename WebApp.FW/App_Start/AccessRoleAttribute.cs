using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApp.FW.App_Start
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Missing XML Comment", "CS1591")]
    public class AccessRoleAttribute : ActionFilterAttribute
    {
        public string Roles { get; set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //base.OnActionExecuting(actionContext);

            //if not requiring specific role return (true)
            if (Roles == null)
                return;

            List<string> myRoles = System.Web.Security.Roles.GetRolesForUser(actionContext.RequestContext.Principal.Identity.Name).ToList();

            if (myRoles.Count() > 0)
            {
                //break out roles and compare for a match
                string[] roles = Roles.Split(',');
                foreach (string role in roles)
                {
                    if (myRoles.Any(x => x == role))
                        return;
                }
            }

            //used Forbidden here instead of Unauthorized, as Unauthorized will cause browser to display a login prompt
            actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            return;
        }
    }
}