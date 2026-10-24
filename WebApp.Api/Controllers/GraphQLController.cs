using EntityGraphQL;
using EntityGraphQL.Schema;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Api.Database;

namespace WebApp.GQL.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GraphQLController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly SchemaProvider<DataContext> _schemaProvider;
        private readonly IServiceProvider _serviceProvider;

        public GraphQLController(
            DataContext context,
            SchemaProvider<DataContext> schemaProvider,
            IServiceProvider serviceProvider
        )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _schemaProvider = schemaProvider ?? throw new ArgumentNullException(nameof(schemaProvider));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }


        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(QueryResult), 200)]
        public object Post([FromBody] QueryRequest gql)
        {
            //QueryResult data = _schemaProvider.ExecuteQuery(gql, _context, _serviceProvider, FakeIdentity(), null);
            QueryResult data = _schemaProvider.ExecuteQuery(gql, _context, _serviceProvider, User.Identities.FirstOrDefault(), null);

            // This work in GraphiQL while also working with axios http status code handling
            if (data.Errors.Count > 0)
                //if (data.Errors.Any(x => x.Message.Contains("You do not have access")))
                //    return Forbid();
                //else
                return BadRequest(new { errors = data.Errors });

            return new JsonResult(data);

            //string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            //string jsonStr = System.Text.Json.JsonSerializer.Serialize(data);
            //var content = new StringContent(jsonStr, System.Text.Encoding.UTF8, "application/json");
            //return new HttpResponseMessage()
            //{
            //    StatusCode = System.Net.HttpStatusCode.OK,
            //    Content = content
            //};
        }

        //// TODO: setup a GET method 
        //[AllowAnonymous]
        //[HttpGet]
        //public object Get()
        //{
        //    return Ok("working");
        //}

        private System.Security.Claims.ClaimsIdentity FakeIdentity()
        {
            var fakeIdentity = new FakeIdentity();
            var identity = new System.Security.Claims.ClaimsIdentity(fakeIdentity);
            identity.AddClaim(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, "Admin"));
            identity.AddClaim(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, "HR"));

            // FakeIdentity already adds the Name
            //identity.AddClaim(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, "user"));
            return identity;
        }

    }

    public class FakeIdentity : System.Security.Principal.IIdentity
    {
        public string AuthenticationType { get => "FAKE"; }

        public bool IsAuthenticated { get => true; }

        public string Name { get => "user"; }
    }
}
