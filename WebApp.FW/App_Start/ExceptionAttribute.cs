using System.Web.Http.Filters;

namespace WebApp.FW
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Missing XML Comment", "CS1591")]
    public class ExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            //Interfaces.IRepository repository = new Services.EFRepository();
            //Interfaces.IEmailClient emailClient = new Services.Email.EmailClient();
            //Interfaces.IEmailTemplate emailTemplate = new Services.Email.EmailTemplate(repository, emailClient);

            //emailTemplate.EmailErrorMessage(actionExecutedContext.Exception,
            //    actionExecutedContext.ActionContext.RequestContext.Principal.Identity.Name,
            //    actionExecutedContext.Request.RequestUri.OriginalString);

            base.OnException(actionExecutedContext);
        }
    }
}