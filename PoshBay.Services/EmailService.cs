using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using PoshBay.Contracts;
using PoshBay.Data.Models;
using PoshBay.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshBay.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfig _emailConfig;

        public EmailService(IOptions<EmailConfig> options)
        {
            _emailConfig = options.Value;
        }
        
        public async Task<bool> SendEmail(RegisterViewModel model)
        {
            MailjetClient client = new MailjetClient(_emailConfig.ApiKey, _emailConfig.ApiSecret);
            MailjetRequest request = new MailjetRequest
            {
                Resource = SendV31.Resource,
            }
                .Property(Send.Messages, new JArray {
                new JObject {
                 {"From", new JObject {
                  {"Email", "ifunanya.onah@thebulbafrica.institute"},
                  {"Name", "IfunanyaOnah"}
                  }},
                 {"To", new JArray {
                  new JObject {
                   {"Email", model.Email},
                   {"Name", model.FullName}
                   }
                  }},
                 {"Subject", "PoshBay Registration"},
                 {"TextPart", "Hi, " + model.FullName + "!\n\n"},
                 {"HTMLPart", EmailBody(model)}
                 }
                });
             MailjetResponse response = await client.PostAsync(request);
            return response.IsSuccessStatusCode;
            //return true;

            //if (response.IsSuccessStatusCode)
            //{
            //    Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
            //    Console.WriteLine(response.GetData());
            //}
            //else
            //{
            //    Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
            //    Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
            //    Console.WriteLine(response.GetData());
            //    Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
            //}
        }



        public async Task<bool> SendResetToKen(string email, string resetLink)
        {
            string body = $"<p>Click the link below to resert your password </p>" +
                            $"<br/><br/>" +
                            $"<a href={resetLink}>Your password reset link</a></p>";


            MailjetClient client = new MailjetClient(_emailConfig.ApiKey, _emailConfig.ApiSecret);
            MailjetRequest request = new MailjetRequest
            {
                Resource = SendV31.Resource,
            }
                .Property(Send.Messages, new JArray {
                new JObject {
                 {"From", new JObject {
                  {"Email", "ifunanya.onah@thebulbafrica.institute"},
                  {"Name", "IfunanyaOnah"}
                  }},
                 {"To", new JArray {
                  new JObject {
                   {"Email", email},
                   {"Name", "User"}
                   }
                  }},
                 {"Subject", "PoshBay Email Reset Link"},
                 {"TextPart", "Hi, hope this email meets you well."},
                 {"HTMLPart", body}
                 }
                });
            
             MailjetResponse response = await client.PostAsync(request);

            return response.IsSuccessStatusCode;
        }

        public string EmailBody(RegisterViewModel model)
        {
            string body = $"<div>" +
                 $"<h4>Welcome to PoshBay.</h4>" +
                 $"<p>While we hold it to be true that money cannot buy everything.</p>" +
                 $"<p>But for all the good things in life that money can buy, <strong>Think PoshBay</Strong>.</p><br/>" +
                 $"<p>Your login credentials are as follow:<p>" +
                 $"<div><strong>Full Name:</strong> {model.FullName}</div>" +
                 $"<div><strong>Email:</strong> {model.Email}</div>"+
                 $"<br><br>" +
                 $"<div><p>You can always reach out to us via <strong>info@poshbay.com</strong></p></div>"+
                 $"<div><strong>Warm Regards, </strong></div><br/><br/>"+
                 $"<div><strong>PoshBay Team</strong></div>"+
                 $"<div>"; 
                
            return body;
        }

    }
}
