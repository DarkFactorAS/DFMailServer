namespace DarkFactor.MailClient
{
    public class EmailAddress
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public EmailAddress()
        {
        }

        public EmailAddress(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}