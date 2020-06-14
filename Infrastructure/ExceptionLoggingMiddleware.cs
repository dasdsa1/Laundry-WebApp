using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Features.Messaging;

namespace WebApplication3.Infrastructure
{
    public class ExceptionLoggingMiddleware
    {
        private readonly IHostingEnvironment _env;
        private readonly IMessageService _messageService;
        private readonly RequestDelegate _next;

        /*public ExceptionLoggingMiddleware(RequestDelegate next, IHostingEnvironment env, IMessageService messageService)
        {
            _env = env;
            _messageService = messageService;
            _next = next;

        }*/

    public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                if (_env.IsDevelopment())
                {
                    throw;
                }

                await _messageService.SendExceptionEmailAsync(e, context);
                context.Response.Redirect("https://mydomain.com");
            }
        }

    }
    
}
