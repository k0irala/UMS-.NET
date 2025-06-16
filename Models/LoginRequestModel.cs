namespace UMS.Models
{
    public class LoginRequestModel
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? CaptchaToken { get; set; } = null;
        public string? CaptchaId { get; set; } = null;
    }
}
