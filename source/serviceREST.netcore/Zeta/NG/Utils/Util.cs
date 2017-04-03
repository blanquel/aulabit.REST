using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NG.FC_DB;
using MimeKit;
using MailKit.Net.Smtp;

namespace NG
{
    //public enum Response
    //{
    //    OK = 1,
    //    NOTPROCCESS = 0,
    //    ERROR = -1,
    //}

    public enum TypeEmail
    {
        CREATE = 1,
        DELETE = 0,

    }
    public enum CATALOG_TABLES
    {
        ZIPCODE_CATALOG = 1,
        //STATUS_ITEMS = 2,
        SCHOOL_SUBJECTS = 2,
        EMAIL = 7,

    }


    public enum RESPONSE_HTTP
    {
        NOFOUND = 404,

    }
    public class Util
    {

    }

    public class Email
    {
        // private DBContext _context;
        private string From { get; set; }
        private string Subject { get; set; }
        private string Body { get; set; }
        public List<string> To { get; set; }
        public TypeEmail typeEmail { get; set; }

        private string smtp { get; set; }
        private int port { get; set; }
        private bool SSL { get; set; } = true;

        private string PWD { get; set; }

        private bool res;
        public bool SendAsync()
        {
            try
            {
                // get config
                using (var _context = new ModeloAula())
                {
                    switch (typeEmail)
                    {
                        case TypeEmail.CREATE:

                            var emailconfigV2 = _context.CATALOG_DETAILS.AsQueryable().Where(x => x.ID_CATALOG_DEFINITION == (int)CATALOG_TABLES.EMAIL && x.ROW_ITEM == (int)typeEmail).Select(x => new { x.FIELD0, x.FIELD1, x.FIELD2, x.FIELD3, x.FIELD4, x.FIELD5 }).FirstOrDefault();
                            this.Subject = emailconfigV2.FIELD0;
                            this.Body = emailconfigV2.FIELD1;
                            this.From = emailconfigV2.FIELD2;
                            this.PWD = emailconfigV2.FIELD3;
                            this.smtp = emailconfigV2.FIELD4;
                            this.port = int.Parse(emailconfigV2.FIELD5);
                            break;
                        case TypeEmail.DELETE:
                            break;
                        default:
                            break;
                    }
                }

                // start 
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("UDO", From));
                if (To == null)
                    throw new Exception("* Se requiere destinatario");
                else
                {
                    foreach (string item in To)
                        message.To.Add(new MailboxAddress(item.Split('@')[0], item));
                }
                message.Subject = Subject;

                message.Body = new TextPart("html")
                {
                    Text = @Body
                };

                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    client.Connect(this.smtp, this.port, false);

                    // Note: since we don't have an OAuth2 token, disable
                    // the XOAUTH2 authentication mechanism.
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate(this.From, this.PWD);

                    client.Send(message);
                    client.Disconnect(true);
                }
                // end 
            }

            catch (Exception ex)
            {
                // TODO add nlog or insight
                Console.WriteLine(ex.Message);
                res = false;
            }
            return res;
        }


    }
}
