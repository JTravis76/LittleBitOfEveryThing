using System.ComponentModel.DataAnnotations;

namespace WebApp.Api.Models
{
    [EntityGraphQL.Schema.MutationArguments]
    public class Login
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required")]
        [System.ComponentModel.Description("")]
        public string Username { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [System.ComponentModel.Description("")]
        public string Password { get; set; } = string.Empty;
    }
}
