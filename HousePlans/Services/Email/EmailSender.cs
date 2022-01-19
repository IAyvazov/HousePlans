namespace HousePlans.Areas.Administration.Services.Email
{
    using HousePlans.Data.Email;
    using HousePlans.Models.Home;
    using MailKit.Net.Smtp;
    using MimeKit;
    using MimeKit.Text;

    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration emailConfig;

        public EmailSender(EmailConfiguration emailConfig)
        {
            this.emailConfig = emailConfig;
        }

        public void SendEmail(EmailModel model)
        {
            var message = new Message(
                model.Email,
                model.Subject,
                model.Content +
                Environment.NewLine +
                Environment.NewLine +
                $"Име: {model.Name}" +
                Environment.NewLine +
                $"Телефон: {model.Phone}" +
                Environment.NewLine +
                $"Имейл: {model.Email}");

            var emailMessage = CreateEmailMessage(message);

            Send(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();

            var mail = new List<MailboxAddress>();
            mail.Add(new MailboxAddress(this.emailConfig.From));

            emailMessage.From.Add(message.To);
            emailMessage.To.AddRange(mail);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(TextFormat.Text) { Text = message.Content };

            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(this.emailConfig.SmtpServer, this.emailConfig.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(this.emailConfig.UserName, this.emailConfig.Password);

                client.Send(mailMessage);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
