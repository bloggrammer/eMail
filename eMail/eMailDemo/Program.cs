using eMail;
using System.Collections.Generic;
using System.Text;

namespace eMailDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var reciepients = new List<EmailCredentials>
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
                                .Authenticate("ANSWER!@#321")
                                .From("Your Name", "test@example.com")
                                .To("John Doe", "john.doe@example.com")
                                .Cc("John Doe", "john.doe@example.com")
                                .Bcc("John Doe", "john.doe@example.com")
                                .ToMany(reciepients)
                                .CcMany(reciepients)
                                .BccMany(reciepients)
                                .AddAttachment("book.pdf")
                                .AddAttachments(new string[] { "book.pdf", "textbook.pdf", "data.txt" })
                                .SendAsHTML("SOMA Logs", sb.ToString());
        }
    }
}
