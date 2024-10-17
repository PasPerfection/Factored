using System;
using System.Net.Mail;
using System.Net;
using FactorX.Core.Models;

namespace FactorX.UI.Services
{
    public class EmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _senderEmail;
        private readonly string _senderPassword;

        public EmailService(string smtpServer, int smtpPort, string senderEmail, string senderPassword)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _senderEmail = senderEmail;
            _senderPassword = senderPassword;
        }

        public void SendFactuurEmail(Factuur factuur, string recipientEmail)
        {
            var subject = $"Factuur {factuur.Nummer}";
            var body = $"Geachte {factuur.Klant.Naam},\n\nHierbij ontvangt u de factuur met nummer {factuur.Nummer}.\n\nMet vriendelijke groet,\nUw bedrijfsnaam";

            SendEmail(recipientEmail, subject, body);
        }

        public void SendOfferteEmail(Offerte offerte, string recipientEmail)
        {
            var subject = $"Offerte {offerte.Nummer}";
            var body = $"Geachte {offerte.Klant.Naam},\n\nHierbij ontvangt u de offerte met nummer {offerte.Nummer}.\n\nMet vriendelijke groet,\nUw bedrijfsnaam";

            SendEmail(recipientEmail, subject, body);
        }

        private void SendEmail(string recipientEmail, string subject, string body)
        {
            using (var client = new SmtpClient(_smtpServer, _smtpPort))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(_senderEmail, _senderPassword);

                var message = new MailMessage(_senderEmail, recipientEmail, subject, body);

                try
                {
                    client.Send(message);
                }
                catch (Exception ex)
                {
                    // Log de fout en gooi deze opnieuw
                    Console.WriteLine($"Fout bij het verzenden van e-mail: {ex.Message}");
                    throw;
                }
            }
        }
    }
}
