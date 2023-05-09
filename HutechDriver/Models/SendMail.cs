using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;

namespace HutechDriver.Models
{
    public class SendMail
    {
        public static void SendEmail(string to, string subject, string body, string attachFile)
        {
            try
            {
                string fromMail = "lokino.00002@gmail.com";
                string fromPassword = "qvmkjgkzfcvfpztu";
                MailMessage msg = new MailMessage("lokino.00002@gmail.com", to, subject, body);
                using (var client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.EnableSsl = true;
                    if(!string.IsNullOrEmpty(attachFile))
                    {
                        Attachment attachment = new Attachment(attachFile);
                        msg.Attachments.Add(attachment);
                    }    
                    NetworkCredential credential = new NetworkCredential(fromMail, fromPassword);
                    client.UseDefaultCredentials = false;
                    client.Credentials = credential;
                    client.Send(msg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}