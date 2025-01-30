using System.Net.Mail;
using System.Net;

namespace CarDealerWebApp.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {            
            try
            {
                // SMTP server settings
                string smtpServer = "smtp.gmail.com"; 
                int port = 587;
                bool enableSsl = true;

                // Sender & Recipient Info
                string senderEmail = email;
                string senderPassword = "mubk thrd kspa rwka"; // Use App Password
                string recipientEmail = "neetagaikwad072@gmail.com";

                // Email Message
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(senderEmail);
                mail.To.Add(recipientEmail);
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = false;

                // SMTP Client
                SmtpClient smtpClient = new SmtpClient(smtpServer, port)
                {
                    Credentials = new NetworkCredential(senderEmail, senderPassword),
                    EnableSsl = enableSsl
                };

                // Send Email
                smtpClient.Send(mail);
                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
