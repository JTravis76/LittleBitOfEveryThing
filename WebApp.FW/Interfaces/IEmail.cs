using System;

namespace WebApp.FW.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEmail
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mailMessage"></param>
        /// <returns></returns>
        bool SendEmail(System.Net.Mail.MailMessage mailMessage);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="IsBodyHtml"></param>
        /// <param name="attachment"></param>
        /// <returns></returns>
        bool SendEmail(
            string to, 
            string from, 
            string subject, 
            string body, 
            bool IsBodyHtml = false,
            System.Net.Mail.Attachment attachment = null);

        /// <summary>
        /// Parses the Exception and sends an HTML email that looks kinda like the JQxhr.response code that is being displayed in the modal.
        /// </summary>
        /// <param name="exc">An exception.  Any exception.</param>
        /// <param name="username">User Identity like; HttpContext.User.Identity.Name</param>
        /// <param name="url">Url where error occured</param>
        void EmailErrorMessage(Exception exc, string username, string url);
    }
}
