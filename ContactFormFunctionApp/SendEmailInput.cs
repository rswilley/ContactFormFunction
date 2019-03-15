namespace ContactFormFunctionApp
{
    public class SendEmailInput
    {
        public string FromDomain { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
