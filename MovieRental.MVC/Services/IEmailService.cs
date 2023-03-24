using MovieRental.MVC.Models.Contact;

namespace MovieRental.MVC.Services
{
    public interface IEmailService
    {
        void Send(EmailMessage message);
    }
}
