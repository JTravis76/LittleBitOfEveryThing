using System.Net.Mail;
using System.Text.RegularExpressions;
using WebApp.Api.Classes;

namespace WebApp.Api.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class EmailSvc : Interfaces.IEmail
    {
        private readonly SmtpClient _client;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="from"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public EmailSvc(SmtpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }


        /// <summary>
        /// Send emails
        /// </summary>
        /// <param name="to">array of email addresses</param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="IsBodyHtml"></param>
        /// <returns>bool True?False</returns>
        public async Task<bool> SendEmail(string[] to, string subject, string body, bool IsBodyHtml = false, Attachment attachment = null)
        {
            try
            {
                var msg = new MailMessage
                {
                    From = new MailAddress(EnvironmentVariables.SmtpFrom),
                    Subject = subject,
                    IsBodyHtml = IsBodyHtml,
                    Body = body
                };

                foreach (string t in to)
                    msg.To.Add(t);

                //msg.CC.Add(new MailAddress("no-reply@domain.net", "Display Name"));

                if (attachment != null)
                    msg.Attachments.Add(attachment);

                await _client.SendMailAsync(msg);

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Send Email
        /// </summary>
        /// <param name="mailMessage">self-constructed mail message</param>
        /// <returns>bool True?False</returns>
        public async Task<bool> SendEmail(MailMessage mailMessage)
        {
            try
            {
                using (var smtp = _client)
                {
                    await smtp.SendMailAsync(mailMessage);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }


        public DotLiquid.Template GetEmailTemplate(string appDirectory, string templatePath)
        {
            templatePath = Path.Combine(appDirectory, templatePath);
            string? templateContent = File.ReadAllText(templatePath);
            return DotLiquid.Template.Parse(templateContent);
        }

        //public static ValidationModel ValidateEmail(string emailAddress)
        //{
        //    if (Regex.IsMatch(emailAddress, EnvironmentVariables.RegexEmail.Get()))
        //    {
        //        return new ValidationModel("Email is formatted correctly", true);
        //    }

        //    return new ValidationModel("Email is formatted incorrectly.", false);
        //}
    }
}
