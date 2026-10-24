namespace WebApp.Api.Models
{
    public class UserModel
    {
        public string? Email { get; set; }
        public bool TwoFactorEnabled { get; set; } = false;
    }
}
