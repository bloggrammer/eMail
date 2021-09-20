# eMail
A simple C# library to quickly send email from any email service.

## Email at a glance
 ```c#
namespace eMail
{
    public class Email
    {
        public Email(string host, int port, bool useSSL = true);

        public Email AddAttachment(string fileName);
        public Email AddAttachments(string[] fileNames);
        public Email Authenticate(string password);
        public Email Bcc(string toName, string toEmail);
        public Email BccMany(IEnumerable<EmailCredentials> credentials);
        public Email Cc(string toName, string toEmail);
        public Email CcMany(IEnumerable<EmailCredentials> credentials);
        public Email From(string fromName, string fromEmail);
        public void SendAsHTML(string subject, string htmlBody);
        public void SendAsText(string subject, string textBody);
        public Email To(string toName, string toEmail);
        public Email ToMany(IEnumerable<EmailCredentials> credentials);
    }
}
   ``` 

## Example Usage I

 ```c#
using eMail;
using System.Collections.Generic;
using System.Text;

namespace eMailDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var recipients = new List<EmailCredentials>
            {
                new EmailCredentials("john.doe@example.com", "John Doe"),
                new EmailCredentials("jane.doe@example.com", "Jane Doe")
            };
            
            var email = new Email("example.com", 465);
            email.Authenticate("password"); //Use the from email account’s password.
            email.From("Your Name", "test@example.com"); //Use the from email address.
            email.To("John Doe", "john.doe@example.com");
            email.Cc("John Doe", "john.doe@example.com");
            email.Bcc("John Doe", "john.doe@example.com");           
            email.ToMany(recipients);
            email.CcMany(recipients);
            email.BccMany(recipients);
            email.AddAttachment("book.pdf");
            email.AddAttachments(new string[] { "book.pdf", "textbook.pdf", "data.txt" });

            email.SendAsText("Demo Email", "Test Demo");
        }
    }
}
   ``` 
   
   ## Example Usage II

 ```c#
using eMail;
using System.Collections.Generic;
using System.Text;

namespace eMailDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var recipients = new List<EmailCredentials>
            {
                new EmailCredentials("john.doe@example.com", "John Doe"),
                new EmailCredentials("jane.doe@example.com", "Jane Doe")
            };

           StringBuilder sb = new StringBuilder();

            sb.Append("<h1 style='border-collapse: collapse;font-family: Arial, Helvetica, sans-serif;'>");
            sb.Append("Test Demo");
            sb.Append("</h1>");
            sb.Append("<p>");
            sb.Append("This is a demo test");
            sb.Append("</p>");

            new Email("example.com", 465)
                                .Authenticate("password"); //Use the from email account’s password.
                                .From("Your Name", "test@example.com"); //Use the from email address.
                                .To("John Doe", "john.doe@example.com")
                                .Cc("John Doe", "john.doe@example.com")
                                .Bcc("John Doe", "john.doe@example.com")
                                .ToMany(reciepients)
                                .CcMany(reciepients)
                                .BccMany(reciepients)
                                .AddAttachment("book.pdf")
                                .AddAttachments(new string[] { "book.pdf", "textbook.pdf", "data.txt" })
                                .SendAsHTML("Demo Email", sb.ToString());
        }
    }
}
   ``` 
