using System;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace winlog
{
    class Mailer
    {
        public void SendMail()
        {
            SmtpClient smtpClient = new SmtpClient();

            string smtpDetails =
                @"
DeliveryMethod = {0},
Host = {1},
PickupDirectoryLocation = {2},
Port = {3},
TargetName = {4},
UseDefaultCredentials = {5}";

            System.Console.WriteLine(smtpDetails,
                smtpClient.DeliveryMethod.ToString(),
                smtpClient.Host,
                smtpClient.PickupDirectoryLocation == null
                    ? "Not Set"
                    : smtpClient.PickupDirectoryLocation.ToString(),
                smtpClient.Port,
                smtpClient.TargetName,
                smtpClient.UseDefaultCredentials.ToString());

        }


        public string SendConfirmation()
        {
            var smtp = new SmtpClient();

            var fromAddress =
                new MailAddress(
                    ConfigurationManager.AppSettings["fromAddress"],
                    ConfigurationManager.AppSettings["fromName"]);

            var toAddress =
                new MailAddress(
                    ConfigurationManager.AppSettings["toAddress"],
                    ConfigurationManager.AppSettings["toName"]);

            string subject = string.Format("{0}",Environment.MachineName);

            string body =   string.Format("--------  INIT LOG  --------\r\n\r\n{0}\r\n{1}\r\n{2}\r\n{3} {4}",
                            Environment.MachineName,
                            Environment.OSVersion,
                            Environment.UserName,
                            DateTime.UtcNow.ToShortDateString(),
                            DateTime.UtcNow.ToShortTimeString());

            var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body };

            try
            {
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "SUCCESS";
        }

        public string SendLog(string content)
        {
            var smtp = new SmtpClient();
            

            var fromAddress = 
                new MailAddress(
                    ConfigurationManager.AppSettings["fromAddress"], 
                    ConfigurationManager.AppSettings["fromName"]);

            var toAddress = 
                new MailAddress(
                    ConfigurationManager.AppSettings["toAddress"],
                    ConfigurationManager.AppSettings["toName"]);

            string subject = string.Format("log {0} {1} UTC", 
                            DateTime.UtcNow.ToShortDateString(), 
                            DateTime.UtcNow.ToShortTimeString());

            string body = content;

            var message = new MailMessage(fromAddress, toAddress) {  Subject = subject, Body = body };
            message.Attachments.Add(Attachment.CreateAttachmentFromString(body, "text/csv"));
            message.Attachments[0].ContentDisposition.FileName = ConfigurationManager.AppSettings["logfile"];
            
                try
                {
                    smtp.Send(message);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }            

            return "SUCCESS";
        }
    }
}
