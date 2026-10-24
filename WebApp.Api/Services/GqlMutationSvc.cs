using EntityGraphQL;
using EntityGraphQL.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApp.Api.Database;

namespace WebApp.Api.Services
{
    public class GqlMutationSvc
    {
        private readonly SchemaProvider<DataContext> _schema;
        private readonly IConfiguration _configuration;

        public GqlMutationSvc(SchemaProvider<DataContext> schema, IConfiguration configuration)
        {
            _schema = schema;
            _configuration = configuration;
        }

        [GraphQLMutation("Authenticate user to server")]
        public Models.Authenticate Authentication(DataContext context, Models.Login login, GraphQLValidator validator)
        {
            List<Claim>? claims = null;
            var authenticate = new Models.Authenticate();

            var user = context.Users
                .Include(x => x.UserRoles)
                .FirstOrDefault(x => x.UserName == login.Username && x.UserPassword == login.Password);

            if (user != null)
            {
                authenticate.Roles = user.UserRoles
                    .Where(x => x.ApplicationName == _configuration["AppSettings:ApplicationName"])
                    .Select(x => x.RoleName)
                    .ToList();

                claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                    new Claim(ClaimTypes.Name, user.UserName!)//<- User.Identity.Name
                    //new Claim("CustomClaim", "myCustomValue")
                };

                foreach (string? role in authenticate.Roles)
                    claims.Add(new Claim(ClaimTypes.Role, role!));

                authenticate.User = user;
            }

            if (claims != null)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                        _configuration["Jwt:Issuer"],
                        claims,
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: credentials);

                authenticate.Token = new JwtSecurityTokenHandler().WriteToken(token);
                return authenticate;
            }

            if (string.IsNullOrEmpty(authenticate.Token))
                validator.AddError("Incorrect username or password");

            return authenticate;
        }
    }
}
