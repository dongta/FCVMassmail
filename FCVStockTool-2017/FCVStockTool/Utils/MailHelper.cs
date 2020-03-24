using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Web.Configuration;
using System.Configuration;
using System.Net.Configuration;
using Microsoft.Exchange.WebServices.Data;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
namespace FCVStockTool.Utils
{
    public class MailHelper
    {
        //public static bool send(string[] toEmail, string subject, string body, List<Attachment> atts, bool isBodyHtml, Configuration config)
        //{
        //    MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
        //    return Send(settings.Smtp.Network.Host, settings.Smtp.Network.Port, settings.Smtp.From, settings.Smtp.Network.Password, settings.Smtp.Network.UserName, toEmail, null, null, subject, body, atts, isBodyHtml);
        //}
        public static bool Send(string host, int port, string fromEmail, string pass, string fromName, string[] toEmail, string[] ccs, string[] bccs, string subject, string body, List<System.Net.Mail.Attachment> atts, bool isBodyHtml)
        {
            // System.Web.Mail.SmtpMail.SmtpServer is obsolete in 2.0
            // System.Net.Mail.SmtpClient is the alternate class for this in 2.0
            SmtpClient smtpClient = new SmtpClient();
            MailMessage message = new MailMessage();

            try
            {
                MailAddress fromAddress = new MailAddress(fromEmail, fromName);

                //From address will be given as a MailAddress Object
                message.From = fromAddress;
                message.SubjectEncoding = Encoding.UTF8;
                message.BodyEncoding = Encoding.UTF8;
                // To address collection of MailAddress
                foreach (string to in toEmail)
                {
                    message.To.Add(to);
                }

                message.Subject = subject;

                // CC and BCC optional
                if (ccs != null)
                    foreach (string cc in ccs)
                    {
                        message.CC.Add(cc);
                    }

                // You can specify Address directly as string
                if (bccs != null)
                    foreach (string bcc in bccs)
                    {
                        message.Bcc.Add(bcc);
                    }
                if (atts != null)
                    foreach (System.Net.Mail.Attachment _att in atts)
                    {
                        message.Attachments.Add(_att);
                    }
                //Body can be Html or text format
                message.IsBodyHtml = isBodyHtml;

                // Message body content
                message.Body = body;

                // You can specify the host name or ipaddress of your server
                if (host.IndexOf("yahoo", StringComparison.OrdinalIgnoreCase) > 0)
                {
                    return sendYahooMail(message, host, port, fromEmail, pass);
                }
                if (host.IndexOf("gmail", StringComparison.OrdinalIgnoreCase) > 0
                    || host.IndexOf("live", StringComparison.OrdinalIgnoreCase) > 0)
                {
                    return sendOtherMail(message, host, port, fromEmail, pass);
                }
                else
                {
                    return sendPOP3(message, host, port, fromEmail, pass);
                }

            }
            catch
            {
                return false;
            }
        }

        private static bool sendYahooMail(MailMessage mailmeassge, string host, int port, string email, string pass)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = host;
                smtpClient.Port = port;//Default port will be 25

                //Credential
                smtpClient.Credentials = new NetworkCredential(email, pass);

                //smtpClient.UseDefaultCredentials = false;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Timeout = 36000;
                smtpClient.EnableSsl = false;
                // Send SMTP mail

                smtpClient.Send(mailmeassge);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private static bool sendPOP3(MailMessage mailmeassge, string host, int port, string email, string pass)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = host;
                //smtpClient.Port = port;//Default port will be 25

                //Credential
                smtpClient.Credentials = new NetworkCredential(email, pass);

                //smtpClient.UseDefaultCredentials = false;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Timeout = 36000;
                smtpClient.EnableSsl = false;
                // Send SMTP mail

                smtpClient.Send(mailmeassge);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private static bool sendOtherMail(MailMessage mailmeassge, string host, int port, string email, string pass)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = host;
                smtpClient.Port = port;//Default port will be 25

                //Credential
                smtpClient.Credentials = new NetworkCredential(email, pass);

                //smtpClient.UseDefaultCredentials = false;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Timeout = 36000;
                smtpClient.EnableSsl = true;
                // Send SMTP mail

                smtpClient.Send(mailmeassge);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool sendExchange(string host, string fromEmail, string username, string pass, string fromName, string[] toEmail, string[] ccs, string[] bccs, string subject, string body, bool isBodyHtml)
        {
            //try
            //{
                ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2007_SP1);

                service.Url = new Uri(@"https://" + host + "/EWS/Exchange.asmx");

                //if have the ip you can get the computer name using the nslookup utility in command line. ->nslookup 192.168.0.90 

                ServicePointManager.ServerCertificateValidationCallback =
                                    delegate(Object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
                                    {
                                        return true;
                                    };

                service.Credentials = new WebCredentials(username, pass);

                EmailMessage mailmessage = new EmailMessage(service);

                mailmessage.From = new EmailAddress(fromName, fromEmail);
                if (toEmail != null)
                    foreach (var item in toEmail)
                    {
                        mailmessage.ToRecipients.Add(item.Trim());
                    }
                if (ccs != null)
                    foreach (var item in ccs)
                    {
                        mailmessage.CcRecipients.Add(item.Trim());
                    }
                if (bccs != null)
                    foreach (var item in bccs)
                    {
                        mailmessage.BccRecipients.Add(item.Trim());
                    }
                //if (atts != null)
                //    foreach (Microsoft.Exchange.WebServices.Data.Attachment _att in atts)
                //    {
                //        mailmessage.Attachments.AddFileAttachment(_att.)
                //    }
                mailmessage.Subject = subject;

                mailmessage.Body = body;

                mailmessage.Body.BodyType = isBodyHtml ? BodyType.HTML : BodyType.Text;

                mailmessage.Send();

                return true;
            //}
            //catch (Exception ex)
            //{
            //    return false;
            //}
        }
    }
}
