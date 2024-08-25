using PrintMaster.Application.Handle.HandleEmail;

namespace PrintMaster.Application.InterfaceServices
{
    public interface IEmailService
    {
        string SendEmail(EmailMessage message);
    }
}
