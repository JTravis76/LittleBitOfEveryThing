using System.Net.Mail;

namespace WebApp.Api.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEmail
    {
        /// <summary>
        /// Send emails
        /// </summary>
        /// <param name="to">array of email addresses</param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="IsBodyHtml"></param>
        /// <param name="attachment"></param>
        /// <returns>bool True?False</returns>
        /// <remarks>From address is set via appsettings.json [Email:Smtp:From]</remarks>
        Task<bool> SendEmail(string[] to, string subject, string body, bool IsBodyHtml = false, Attachment attachment = null);

        /// <summary>
        /// Send Email
        /// </summary>
        /// <param name="mailMessage">self-constructed mail message</param>
        /// <returns>bool True?False</returns>
        Task<bool> SendEmail(MailMessage mailMessage);

        /// <summary>
        /// Fetches the DotLiquid template
        /// </summary>
        /// <param name="appDirectory"></param>
        /// <param name="templatePath"></param>
        /// <returns></returns>
        public DotLiquid.Template GetEmailTemplate(string appDirectory, string templatePath);
    }
}
