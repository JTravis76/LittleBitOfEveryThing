using DotLiquid;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebAppFunction.Api.Classes;
using WebAppFunction.Api.Models;
using WebAppFunction.Api.Services.Interfaces;

namespace WebAppFunction.Api.Services;

public class EmailService : IEmailService
{
  private static readonly string EmailFromAddress = EnvironmentVariables.DefaultEmailFromAddress.Get();
  private static readonly string EmailFromName = EnvironmentVariables.DefaultEmailFromName.Get();

  public async Task SendEmailAsync(SendGridMessage message)
  {
    var sendGridApiKey = EnvironmentVariables.SendGridApiKey.Get();

    message.From ??= new EmailAddress(EmailFromAddress, EmailFromName ?? EmailFromAddress);

    var sendGridClient = new SendGridClient(sendGridApiKey);
    var response = await sendGridClient.SendEmailAsync(message);

    if (!response.IsSuccessStatusCode)
      throw new Exception(await response.Body.ReadAsStringAsync());
  }

  public static Template GetEmailTemplate(string functionAppDirectory, string templatePath)
  {
    templatePath = Path.Combine(functionAppDirectory, templatePath);
    var templateContent = File.ReadAllText(templatePath);
    return Template.Parse(templateContent);
  }

  public static ValidationModel ValidateEmail(string emailAddress)
  {
    if (Regex.IsMatch(emailAddress, EnvironmentVariables.RegexEmail.Get()))
    {
      return new ValidationModel("Email is formatted correctly", true);
    }

    return new ValidationModel("Email is formatted incorrectly.", false);
  }
}