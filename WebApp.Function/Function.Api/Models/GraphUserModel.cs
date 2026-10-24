using System.Collections.Generic;

namespace WebAppFunction.Api.Models
{
    public class GraphUserModel : Entity
    {
        public GraphUserModel() => this.ODataType = "microsoft.graph.user";

        [Newtonsoft.Json.JsonProperty("value")]
        public List<GraphUser> Value { get; set; }
    }

    public class GraphUser
    {
        [Newtonsoft.Json.JsonProperty("id")]
        public string Id { get; set; }

        [Newtonsoft.Json.JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [Newtonsoft.Json.JsonProperty("mail")]
        public string Mail { get; set; }

        [Newtonsoft.Json.JsonProperty("surname")]
        public string Surname { get; set; }

        [Newtonsoft.Json.JsonProperty("givenName")]
        public string GivenName { get; set; }
    }

    public class Entity
    {
        [Newtonsoft.Json.JsonProperty("@odata.type")]
        public string ODataType { get; set; }
    }
}
