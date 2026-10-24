using System.ComponentModel;

namespace WebApp.Api.Models
{
    public class Authenticate
    {
        [Description("Authentication token")]
        public string Token { get; set; } = string.Empty;

        [Description("Collection of user's roles")]
        public List<string?> Roles { get; set; } = new List<string?>();

        [Description("User object")]
        public Entities.User? User { get; set; } = null;
    }
}
