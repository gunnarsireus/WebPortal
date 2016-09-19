using System;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

using ServerLibrary.Model;

namespace ServerLibrary.Utils
{
    public class MailUtils
    {
        // Singleton
        private static readonly Lazy<MailUtils> lazy = new Lazy<MailUtils>(() => new MailUtils());
        public static MailUtils Instance { get { return lazy.Value; } }

        // Instance variables
        private string mailaccount  = "";
        private string mailpassword = "";
        private string mailserver   = "";
        private int    mailport     = 0;

        private MailUtils()
        {
            mailaccount  = "renew@itancan.com";
            mailpassword = "Itancan2014!";
            mailserver   = "smtp.itancan.com";
            mailport     = 587;
        }

        public bool IsValid(string emailaddress)
        {
            //regex code from MSDN email validation example, see https://msdn.microsoft.com/en-us/library/01escwtf.aspx as to valid and invalid email examples
            return Regex.IsMatch(emailaddress, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        
        // Returns "null" att success, else message to be sent to trace log
        public string SendIssueTransitionNotification(Account receiver, Issue report, IssueTransition transition)
        {
            string subject = "TBD";
            string body    = "TBD";
            return this.Send(receiver.email, receiver.Name, subject, body);
        }

        // Returns "null" att success, else message to be sent to trace log
        public string Send_PIN(Account receiver)
        {
            string subject = "Renew Service - Din PIN-kod";
            string body    = "Din PIN-kod är: " + receiver.PIN + ". Koden är giltig i 30 minuter. ";
            return this.Send(receiver.email, receiver.Name, subject, body);
        }

        private string Send(string mail, string name, string subject, string body)
        {
            MailAddress sender   = new MailAddress(this.mailaccount, "Renew Service AB");
            MailAddress receiver = new MailAddress(mail, name);
            var client = new SmtpClient
            {
                Host = this.mailserver,
                Port = this.mailport,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(this.mailaccount, this.mailpassword),
            };
            using (var message = new MailMessage(sender, receiver)
            {
                Subject = subject,
                Body = body
            })
            {
                try
                {
                    client.Send(message);
                    return null;
                }
                catch (Exception e)
                {
                    return("MailUtils.Send() " + e.ToString());
                }
            }
        }
    }
}