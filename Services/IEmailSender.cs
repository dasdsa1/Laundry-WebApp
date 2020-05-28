using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Services
{
    public interface IEmailSender
    {
        void Send(string toAddress, string subject, string body, bool sendAsync = true);


    }
}
