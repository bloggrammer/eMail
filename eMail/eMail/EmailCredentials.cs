namespace eMail
{
    public class EmailCredentials
    {
        public EmailCredentials(string emailAddress, string name=null)
        {
            EmailAddress = emailAddress;
            Name = name;
        }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
    }
}
