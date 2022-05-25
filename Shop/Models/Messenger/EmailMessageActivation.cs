﻿using System;
using System.Collections.Generic;
using System.Linq;
using MailKit.Net.Smtp;

using System.Text;
using System.Threading.Tasks;
using Shop.Models.Configs;
using MimeKit;

namespace Shop.Models.Messenger
{
    public class EmailMessageActivation : IMessenger <EmailMessageActivation>
    {
        private readonly EmailConfig emailConfig;

        public EmailMessageActivation(EmailConfig emailConfig)
        {
            this.emailConfig = emailConfig;
        }
        public async Task sendMessageAsync(string message, string recipient)
        {
            using ( var client = new SmtpClient())
            {
                
                    
                    client.Connect(emailConfig.Host, emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(emailConfig.Mail, emailConfig.Password);
                    var ms = new MimeMessage();
                    ms.From.Add(MailboxAddress.Parse(emailConfig.Mail));
                    ms.To.Add(MailboxAddress.Parse(recipient));
                    ms.Subject = "Activation Account";
                    ms.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message};
                    client.Send(ms);
                    client.Disconnect(true);
                    client.Dispose();
                    
            }
        }
    }
}
