using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Features.Messaging.Email;
using WebApplication3.Infrastructure;

namespace WebApplication3.Features.Messaging
{
    public class MessageService : IMessageService
    {
        private readonly IViewRenderer _viewRenderer;

        public MessageService(IViewRenderer viewRenderer)
        {
            _viewRenderer = viewRenderer;
        }

        public async Task SendEmailAsync(string fromDisplayName,
            string fromEmailAddress,
            string toName,
            string toEmailAddress,
            string subject,
            string message,
            params Attachment[] attachments)
        {

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(fromDisplayName, fromEmailAddress));
            email.To.Add(new MailboxAddress(toName, toEmailAddress));
            email.Subject = subject;

            var body = new BodyBuilder
            {
                HtmlBody = message
            };

            foreach (var attachment in attachments)
            {
                using (var stream = await attachment.ContentToStreamAsync())
                {
                    body.Attachments.Add(attachment.FileName, stream);
                }

            }

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (sender, certificate, certChainType, errors) => true;
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                await client.ConnectAsync("smtp.host", 587, false).ConfigureAwait(false);
                await client.AuthenticateAsync("username", "password").ConfigureAwait(false);

                await client.SendAsync(email).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);

            }

        }

        public async Task SendEmailToSupportAsync(string subject, string message)
        {
            await SendEmailAsync("No Reply", "no-reply@domain.com", "Support", "support@domain.com", subject, message);
        }

        public async Task SendExceptionEmailAsync(Exception e, HttpContext context)
        {
            var message = _viewRenderer.Render("ExceptionEmail", new ExceptionEmailModel(e, context));
            await SendEmailToSupportAsync("Exception", message);


        }
    }
}
