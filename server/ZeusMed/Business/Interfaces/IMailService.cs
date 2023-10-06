using Business.DTOs;

namespace Business.Interfaces;

public interface IMailService
{
    Task SendEmailAsync(MailRequestDto mailRequest);
}