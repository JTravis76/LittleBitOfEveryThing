using System.Threading.Tasks;
using SendGrid.Helpers.Mail;

namespace WebAppFunction.Api.Services.Interfaces;

public interface IEmailService
{
  public Task SendEmailAsync(SendGridMessage message);
}