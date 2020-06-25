using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace WebApplication3.Services
{
    public class AmazonEmailSender : IEmailSender
    {



        public string FROM = "paulotavoraa@gmail.com";
        public string FROMNAME = "Paulo";

        // Replace recipient@example.com with a "To" address. If your account 
        // is still in the sandbox, this address must be verified.
       
        // Replace smtp_username with your Amazon SES SMTP user name.
        public string SMTP_USERNAME = "paulotavoraa@gmail.com";

        // Replace smtp_password with your Amazon SES SMTP user name.
        public string SMTP_PASSWORD = "pTavora1";

        // (Optional) the name of a configuration set to use for this message.
        // If you comment out this line, you also need to remove or comment out
        // the "X-SES-CONFIGURATION-SET" header below.
        public string CONFIGSET = "ConfigSet";

        // If you're using Amazon SES in a region other than US West (Oregon), 
        // replace email-smtp.us-west-2.amazonaws.com with the Amazon SES SMTP  
        // endpoint in the appropriate AWS Region.
        public string HOST = "smtp.gmail.com";

        // The port you will connect to on the Amazon SES SMTP endpoint. We
        // are choosing port 587 because we will use STARTTLS to encrypt
        // the connection.
        public int PORT = 587;

        // The subject line of the email
        public string SUBJECT =
            "Amazon SES test (SMTP interface accessed using C#)";

    

        public void Send(string toAddress, string subject, string body, bool sendAsync = true)
        {
            var defaultValues = new AmazonEmailSender();

            MailMessage message = new MailMessage();

            message.IsBodyHtml = true;

            message.From = new MailAddress(defaultValues.FROM, defaultValues.FROMNAME);

            message.To.Add(new MailAddress(toAddress));
            message.Subject = subject;
            message.Body = body;
            message.Headers.Add("X-SES-CONFIGURATION-SET", defaultValues.CONFIGSET);

            using (var client = new SmtpClient(defaultValues.HOST, defaultValues.PORT))
            {
                client.Credentials = new NetworkCredential(defaultValues.SMTP_USERNAME, defaultValues.SMTP_PASSWORD);

                client.EnableSsl = true;


                client.Send(message);


            }


        }

      


    }
}
