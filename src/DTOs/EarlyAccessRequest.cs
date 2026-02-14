namespace GamePadCMS_Website.DTOs
{
    public class EarlyAccessRequest
    {
        public required string FullName { get; set; }
        public required string EmailAddress { get; set; }
        public string? CompanyName { get; set; }
        public string? Role { get; set; }
        public string? Details { get; set; }
    }
}
