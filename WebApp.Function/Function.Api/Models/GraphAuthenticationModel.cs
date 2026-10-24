namespace WebAppFunction.Api.Models
{
    public class GraphAuthenticationModel
    {
        [Newtonsoft.Json.JsonProperty("token_type")]
        public string TokenType { get; set; }

        [Newtonsoft.Json.JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [Newtonsoft.Json.JsonProperty("ext_expires_in")]
        public int ExtExpiresIn { get; set; }

        [Newtonsoft.Json.JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
