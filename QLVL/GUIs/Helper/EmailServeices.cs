using Microsoft.VisualBasic.CompilerServices;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace testLib
{
    public class EmailServeices
    {
        private string _ServSMTP;
        private string _mail_to;
        private string _mail_from;
        private string _password;
        private string _chude;
        private string _noidung;

        [DebuggerNonUserCode]
        public EmailServeices()
        {
        }

        public string ServSMTP
        {
            get => this._ServSMTP;
            set => this._ServSMTP = value;
        }

        public string MailTo
        {
            get => this._mail_to;
            set => this._mail_to = value;
        }

        public string MailFrom
        {
            get => this._mail_from;
            set => this._mail_from = value;
        }

        public string Chude
        {
            get => this._chude;
            set => this._chude = value;
        }

        public string Password
        {
            get => this._password;
            set => this._password = value;
        }

        public string Noidung
        {
            get => this._noidung;
            set => this._noidung = value;
        }

        public bool IsEmail(string Email)
        {
            string[] strArray = Email.Split('@');
            if (strArray.Length != 2)
                return false;
            return strArray[1].Split('.').Length >= 2;
        }

        public void SendMail()
        {
            MailMessage message = new MailMessage();
            message.BodyEncoding = Encoding.UTF8;
            message.From = new MailAddress(this._mail_from);
            message.Subject = this._chude;
            message.To.Add(new MailAddress(this._mail_to));
            message.Body = this._noidung;
            SmtpClient smtpClient = new SmtpClient();
            new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                Credentials = ((ICredentialsByHost)new NetworkCredential(this._mail_from, this._password))
            }.Send(message);
            message.Dispose();
        }

        public void SendMail(string[] fileAttach)
        {
            MailMessage message = new MailMessage();
            message.BodyEncoding = Encoding.UTF8;
            message.From = new MailAddress(this._mail_from);
            message.Subject = this._chude;
            message.To.Add(new MailAddress(this._mail_to));
            message.Body = this._noidung;
            SmtpClient smtpClient1 = new SmtpClient();
            SmtpClient smtpClient2 = new SmtpClient();
            smtpClient2.Host = "smtp.gmail.com";
            smtpClient2.Port = 587;
            smtpClient2.Credentials = (ICredentialsByHost)new NetworkCredential(this._mail_from, this._password);
            int num = checked(fileAttach.Length - 1);
            int index = 0;
            while (index <= num)
            {
                if (System.IO.File.Exists(fileAttach[index]))
                {
                    Attachment attachment = new Attachment(fileAttach[index]);
                    message.Attachments.Add(attachment);
                }
                checked { ++index; }
            }
            smtpClient2.EnableSsl = true;
            smtpClient2.Send(message);
            message.Dispose();
        }

        public void SendMail(string fileAttach, string[] email)
        {
            MailMessage message = new MailMessage();
            message.BodyEncoding = Encoding.UTF8;
            message.From = new MailAddress(this._mail_from);
            message.Subject = this._chude;
            message.Body = this._noidung;
            SmtpClient smtpClient1 = new SmtpClient();
            SmtpClient smtpClient2 = new SmtpClient();
            smtpClient2.Host = "smtp.gmail.com";
            smtpClient2.Port = 587;
            smtpClient2.Credentials = (ICredentialsByHost)new NetworkCredential(this._mail_from, this._password);
            Attachment attachment = new Attachment(fileAttach);
            message.Attachments.Add(attachment);
            int num = checked(email.Length - 1);
            int index = 0;
            while (index <= num)
            {
                if (Operators.CompareString(email[index], "", false) != 0)
                    message.To.Add(email[index]);
                checked { ++index; }
            }
            smtpClient2.EnableSsl = true;
            smtpClient2.Send(message);
            message.Dispose();
        }
    }
}