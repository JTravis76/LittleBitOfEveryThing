using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WebApp.FW.Interfaces;

namespace WebApp.FW.Services
{
    public class EmailSvc : IEmail
    {
        /// <summary>
        /// Send emails, Mail server settings are in app.config / web.config
        /// </summary>
        /// <param name="to">single email address</param>
        /// <param name="from"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="IsBodyHtml"></param>
        /// <param name="attachment"></param>
        /// <returns>bool True?False</returns>
        public bool SendEmail(
            string to,
            string from,
            string subject,
            string body,
            bool IsBodyHtml = false,
            System.Net.Mail.Attachment attachment = null)
        {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            try
            {
                msg.From = new System.Net.Mail.MailAddress(from);
                msg.To.Add(to);
                msg.Subject = subject;
                msg.IsBodyHtml = IsBodyHtml;
                msg.Body = body;
                if (attachment != null)
                    msg.Attachments.Add(attachment);

                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                client.Send(msg);

                return true;
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mailMessage"></param>
        /// <returns></returns>
        public bool SendEmail(System.Net.Mail.MailMessage mailMessage)
        {
            try
            {
                var client = new System.Net.Mail.SmtpClient();
                client.Send(mailMessage);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Parses the Exception and sends an HTML email that looks kinda like the JQxhr.response code that is being displayed in the modal.
        /// </summary>
        /// <param name="exc">An exception.  Any exception.</param>
        /// <param name="username">User Identity like; HttpContext.User.Identity.Name</param>
        /// <param name="url">Url where error occured</param>
        public void EmailErrorMessage(Exception exc, string username, string url)
        {
            string instance = System.Configuration.ConfigurationManager.AppSettings["Environment"];

            if (instance == "LOCAL")
                return;


            StringBuilder sb = new StringBuilder();

            sb.AppendLine("The following error was received in " + instance + " instance by: " + username.ToUpper() + " On " + DateTime.Now);
            sb.AppendLine("<span style=\"font-size:18px;font-weight:bold\"><br />URL: </span><span>" + url + "</span>");

            var stackTrace = new System.Diagnostics.StackTrace(exc, true);
            System.Diagnostics.StackFrame currentFrame = stackTrace.GetFrame(0);
            string fname = currentFrame.GetFileName();
            int lineNum = currentFrame.GetFileLineNumber() - 1;

            //give us that nice tabbed effect.
            string result = System.Text.RegularExpressions.Regex.Replace(exc.StackTrace, @"\r\n?|\n", "<br />&nbsp&nbsp&nbsp&nbsp&nbsp");

            sb.AppendLine("<br/> <h1>" + SanitizeHTML(exc.Message) + "</h1>");
            sb.AppendLine("<span style=\"font-size:18px;font-weight:bold\">Description: </span><br /> An unhandled exception occurred during the execution of the current web request.  Please review the stack trace for more information about the error and where it originated in the code. ");

            sb.AppendLine("<br /><br /><span style=\"font-size:18px;font-weight:bold\">Exception Details:</span><span> " + exc.GetType() + " : " + exc.Message + "</span>");

            //If there is a filename, this will grab the file, open it, and grab the 2 lines before, and 2 lines after the line that the error is occuring on.
            if (fname != null)
            {
                //Create the code block.
                sb.AppendLine("<br /><br /><span style=\"font-size:18px;font-weight:bold\">Source Error:<br /></span>");
                sb.AppendLine("<div style =\"background-color:lightgray;border:1px solid\">");
                sb.AppendLine("<pre><code>");

                int lineCount = System.IO.File.ReadLines(fname).Count();
                //-2
                if (lineNum - 2 > 0)
                {
                    string line1 = SanitizeHTML(System.IO.File.ReadLines(fname).Skip(lineNum - 2).Take(1).First());
                    sb.AppendLine("Line " + (lineNum - 1) + ":" + line1);
                }
                //-1
                if (lineNum - 1 > 0)
                {
                    string line2 = SanitizeHTML(System.IO.File.ReadLines(fname).Skip(lineNum - 1).Take(1).First());
                    sb.AppendLine("Line " + (lineNum) + ":" + line2);
                }
                //0 :This is the line that the error occurred on.                
                string line3 = SanitizeHTML(System.IO.File.ReadLines(fname).Skip(lineNum).Take(1).First());
                sb.AppendLine("<span style=\"color:red;\">Line " + (lineNum + 1) + ":" + line3 + " </span>");
                //+1
                if (lineNum + 1 < lineCount)
                {
                    string line4 = SanitizeHTML(System.IO.File.ReadLines(fname).Skip(lineNum + 1).Take(1).First());
                    sb.AppendLine("Line " + (lineNum + 2) + ":" + line4);
                }
                //+2
                if (lineNum + 2 < lineCount)
                {
                    string line5 = SanitizeHTML(System.IO.File.ReadLines(fname).Skip(lineNum + 2).Take(1).First());
                    sb.AppendLine("Line " + (lineNum + 3) + ":" + line5);
                }
                //close the code block
                sb.AppendLine("</pre></code></div>");
                sb.AppendLine("<br /><span style=\"font-size:18px;font-weight:bold\">Source File: </span>" + fname + "<span style=\"font-size:18px;font-weight:bold\"> Line: </span>" + (lineNum + 1));
            }
            //For the stack trace.
            sb.AppendLine("<br /><h3>Stack trace: </h3>\r\n"
                        + "<div style =\"background-color:lightgray;border:1px solid\">" + "[" + exc.GetType() + " " + exc.Message + "]"
                        + "<br />&nbsp&nbsp&nbsp&nbsp&nbsp" + result);
            //In case the stacktrace isn't good enough, here is the inner exception.
            //TODO: May need some better formatting later on, Don't really have a clue how these will look.
            if (exc.InnerException != null)
            {
                sb.AppendLine("<br />---BEGIN InnerException--- ");
                sb.AppendLine("<br /><h4>Exception type</h4> " + exc.InnerException.GetType());
                sb.AppendLine("<br /><h4>Exception message:</h4> " + exc.InnerException.Message);
                sb.AppendLine("<br /><h4>Stack trace:</h4> " + exc.InnerException.StackTrace);
                sb.AppendLine("<br />---END Inner Exception");
            }
            //Close everything up.
            sb.AppendLine("</div>");
            sb.AppendLine("<p></p>");

            SendEmail("support@domain.net",
                "no-reply@domain.net",
                $"Application error in {instance}",
                sb.ToString(),
                true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="builderToFill"></param>
        /// <param name="level"></param>
        public void WriteExceptionDetails(Exception exception, StringBuilder builderToFill, int level)
        {
            var indent = new string(' ', level);

            if (level > 0)
            {
                builderToFill.AppendLine(indent + "=== INNER EXCEPTION ===");
            }

            void append(string prop)
            {
                var propInfo = exception.GetType().GetProperty(prop);
                var val = propInfo.GetValue(exception);

                if (val != null)
                {
                    builderToFill.AppendFormat("{0}{1}: {2}{3}", indent, prop, val.ToString(), Environment.NewLine);
                }
            }

            append("Message");
            append("HResult");
            append("HelpLink");
            append("Source");
            append("StackTrace");
            append("TargetSite");

            foreach (System.Collections.DictionaryEntry de in exception.Data)
            {
                builderToFill.AppendFormat("{0} {1} = {2}{3}", indent, de.Key, de.Value, Environment.NewLine);
            }

            if (exception.InnerException != null)
            {
                WriteExceptionDetails(exception.InnerException, builderToFill, ++level);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encodedString"></param>
        /// <returns></returns>
        private string SanitizeHTML(string encodedString)
        {
            string line = encodedString;
            line = line.Replace("&", "&amp;");
            line = line.Replace("<", "&lt;");
            line = line.Replace(">", "&gt;");
            return line;
        }
    }
}