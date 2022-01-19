namespace HousePlans.Areas.Administration.Services.Email
{
    using HousePlans.Models.Home;

    public interface IEmailSender
    {
        void SendEmail(EmailModel message);
    }
}
