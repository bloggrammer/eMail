using MimeKit;
using MailKit.Net.Smtp;
using System.Linq;
using System.Collections.Generic;
using System;

namespace eMail
{
    public class Email
    {
        public Email(string host,int port, bool useSSL=true)
        {
            _host = host;
            _port = port;
            _useSSL = useSSL;
            _mailMessage = new MimeMessage();
        }
        public Email Authenticate(string password)
        {
            _fromPassword = password;
            return this;
        }
        public Email From(string fromName, string fromEmail)
        {
            _fromName = fromName;
            _fromEmail = fromEmail;

            _mailMessage.From.Add(new MailboxAddress(_fromName, _fromEmail));
            return this;
        }

        public Email To(string toName, string toEmail)
        {
            _mailMessage.To.Add(new MailboxAddress(toName, toEmail));
            return this;
        }
        public Email ToMany(IEnumerable<EmailCredentials> credentials)
        {
            _mailMessage.To.AddRange(credentials.Select(receiver => new MailboxAddress(receiver?.Name, receiver.EmailAddress)));
            return this;
        }
        public Email Cc(string toName, string toEmail)
        {
            _mailMessage.Cc.Add(new MailboxAddress(toName, toEmail));
            return this;
        }


        public Email CcMany(IEnumerable<EmailCredentials> credentials)
        {
            _mailMessage.Cc.AddRange(credentials.Select(receiver => new MailboxAddress(receiver?.Name, receiver.EmailAddress)));
            return this;
        }
        public Email Bcc(string toName, string toEmail)
        {
            _mailMessage.Bcc.Add(new MailboxAddress(toName, toEmail));
            return this;
        }


        public Email BccMany(IEnumerable<EmailCredentials> credentials)
        {
            _mailMessage.Bcc.AddRange(credentials.Select(receiver => new MailboxAddress(receiver?.Name, receiver.EmailAddress)));
            return this;
        }

        public Email AddAttachment(string fileName)
        {
            if (_builder == null) _builder = new BodyBuilder();

            _builder.Attachments.Add(fileName);
            return this;
        }

        public Email AddAttachments(string[] fileNames)
        {
            if (_builder == null) _builder = new BodyBuilder();

            foreach (var fileName in fileNames)
                _builder.Attachments.Add(fileName);

            return this;
        }

        public void SendAsText(string subject, string textBody)
        {
            ValidateParams();

            if (_builder == null || _builder.Attachments.Count < 1)
                _mailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Plain) { Text = textBody };
            else
            {
                _builder.TextBody = textBody;
                _mailMessage.Body = _builder.ToMessageBody();
            }
            Send(subject);
        }
        public void SendAsHTML(string subject, string htmlBody)
        {
            ValidateParams();

            if (_builder == null || _builder.Attachments.Count < 1)
                _mailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlBody };
            else
            {
                _builder.HtmlBody = htmlBody;
                _mailMessage.Body = _builder.ToMessageBody();
            }
            Send(subject);
        }

        private void Send(string subject)
        {
            _mailMessage.Subject = subject;
            using (var smtpClient = new SmtpClient())
            {
                try
                {
                    smtpClient.Connect(_host, _port, _useSSL);
                    smtpClient.Authenticate(_fromEmail, _fromPassword);
                    smtpClient.Send(_mailMessage);
                    smtpClient.Disconnect(true);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex.InnerException);
                }
            }
        }
        private void ValidateParams()
        {           
            if (string.IsNullOrWhiteSpace(_fromEmail) || string.IsNullOrWhiteSpace(_fromPassword))
                throw new ArgumentException("\"From\" email or password is invalid.");
            if (string.IsNullOrWhiteSpace(_fromName))
                _fromName = _fromEmail;
        }
        private readonly string _host;
        private readonly int _port;
        private readonly bool _useSSL;
        private string _fromName;
        private string _fromEmail;
        private string _fromPassword;
        private readonly MimeMessage _mailMessage;
        private BodyBuilder _builder;
    }
}
