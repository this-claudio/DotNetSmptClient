using System.ComponentModel;
using System.Net;
using System.Net.Mail;

namespace EmailClient
{

    public class MailClient
    {
        private string SmtpServer { get; set; }
        private string Name { get; set; }
        private string Email { get; set; }
        private string Password { get; set; }

        public MailClient(string smtpServer, string name, string email, string password)
        {
            this.SmtpServer = smtpServer;
            this.Name = name;
            this.Email = email;
            this.Password = password;
        }

        public void SendEmail(string To, string Subject, string Message)
        {

            SmtpClient client = new SmtpClient(this.SmtpServer)
            {
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential(this.Email, this.Password)
            };


            MailAddress from = new MailAddress(this.Email, this.Name,
            System.Text.Encoding.UTF8);

            MailAddress to = new MailAddress(To);

            MailMessage message = new MailMessage(from,to);

            message.Body = Message;
            message.BodyEncoding = System.Text.Encoding.UTF8;

            message.Subject = Subject;
            message.SubjectEncoding = System.Text.Encoding.UTF8;


            client.SendCompleted += new
            SendCompletedEventHandler(SendCompletedCallback);

            string userState = string.Empty;
            client.SendAsync(message, userState);
        }

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message sent.");
            }
        }
    }
}
