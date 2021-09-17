# eMail
A simple C# library to quickly send email from any email service.

## Example Usage

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

            var email = new Email("example.com", 465);
            email.Authenticate("password"); //Use the from email account’s password.
            email.From("Your Name", "test@example.com");
            email.To("John Doe", "john.doe@example.com");
            email.Cc("John Doe", "john.doe@example.com");
            email.Bcc("John Doe", "john.doe@example.com");           
            email.ToMany(reciepients);
            email.CcMany(reciepients);
            email.BccMany(reciepients);
            email.AddAttachment("book.pdf");
            email.AddAttachments(new string[] { "book.pdf", "textbook.pdf", "data.txt" });

            email.SendAsText("SOMA Logs", "Test Demo");

            email.SendAsHTML("SOMA Logs", sb.ToString());
        }
    }
}


   ``` 
