using System.ComponentModel.DataAnnotations;

namespace WebApp.Api.Models
{
    public class NpmUser
    {
        [System.Text.Json.Serialization.JsonPropertyName("_id")]
        public string? _Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        [System.Text.Json.Serialization.JsonPropertyName("name")]
        public string? Name { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("password")]
        public string? Password { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("type")]
        public string? Type { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("roles")]
        public string[]? Roles { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("date")]
        public DateTime Date { get; set; }
    }
}
