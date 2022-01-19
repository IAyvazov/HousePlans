namespace HousePlans.Data.Email
{
    using MimeKit;

    public class Message
    {
        public Message(string to, string subject, string content)
        {
            this.To = new MailboxAddress(to);
            this.Subject = subject;
            this.Content = content;
        }

        public MailboxAddress To { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }
    }
}
