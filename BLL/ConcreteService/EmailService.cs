using BLL.AbstractService;
using BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ConcreteService
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpUser;
        private readonly string _smtpPass;
        // Email service e  ctor açtığımız için ctor verileri verilmeden oluşturulması mümkün değildir. bu yüzden progrmacs. te tanımlama yaparız. 
        public EmailService(string smtpUser, string smtpPass)
        {
            _smtpUser = smtpUser;
            _smtpPass = smtpPass;
        }
        public async Task SendEmailAsync(EmailRequestDto email)
        {
            // yeni bir stmp client oluşturuyoruz.
            var smtpClient = new SmtpClient("smtp.gmail.com") //Gmail in stmp servisi gibi düşünebiliriz.
            {
                Port = 587,
                Credentials = new NetworkCredential(_smtpUser, _smtpPass), // Gmail username ve şifre gibi düşünebiliriz.
                EnableSsl = true            // Güvenlik sertifikası
            };
            //Yeni bir mail nesnesi oluşturuyoruz.
            var mailMessage = new MailMessage()
            {
                From = new MailAddress(email.From),
                Subject = email.Subject,
                Body = email.Body,
                IsBodyHtml = email.IsBodyHtml
            };
            mailMessage.To.Add(email.To);      //Mailin gönderileceği kişi.
            try
            {
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException("Failed To Send email", ex);
            }
        }
    }
}
