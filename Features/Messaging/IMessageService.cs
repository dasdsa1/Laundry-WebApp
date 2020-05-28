using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Features.Messaging
{
    public interface IMessageService
    {
        Task SendEmailAsync(
            string fromDisplayName,
            string fromEmailAddress,
            string toName,
            string toEmailAddress,
            string Subject,
            string message,
            params Attachment[] attachments);
        Task SendEmailToSupportAsync(string subject, string message);

        Task SendExceptionEmailAsync(Exception e, HttpContext context);
    }
}
