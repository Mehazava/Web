using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messages.Services
{
    public class SmsMessageSender : IMessageSender
    {
        public string Send(HttpContext context)
        {
            string WhatGot = context.Request.Cookies["text"];
            if (String.IsNullOrWhiteSpace(WhatGot))
            {
                return "text empty";
            }
            return WhatGot;
        }
    }
}
