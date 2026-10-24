using EntityGraphQL.Schema;
using WebApp.Api.Database;
using WebApp.Api.Interfaces;

namespace WebApp.Api.Services
{
    public class GqlSvc : IGql
    {
        private readonly IConfiguration _configuration;

        public GqlSvc(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public SchemaProvider<DataContext> MakeSchema()
        {
            // Build the Schema(code)-First graphql model
            var schema = SchemaBuilder.FromObject<DataContext>();

            /*== Scalar Types ==*/
            //schema.AddCustomScalarType(typeof(byte), "Byte", "Represents an 8-bit unsigned integer", false);
            //schema.AddCustomScalarType(typeof(DateTime), "DateTime", "Represents an instant of time", false);
            //schema.AddCustomScalarType(typeof(long), "Long", "Represents an 64-bit signed integer", false);

            schema.AddScalarType<byte>("Byte", "Represents an 8-bit unsigned integer");
            schema.AddScalarType<DateTime>("DateTime", "Represents an instant of time");
            schema.AddScalarType<long>("Long", "Represents an 64-bit signed integer");

            /*== INPUT TYPES ==*/
            schema.AddInputType<Models.Login>(nameof(Models.Login), "Login model").AddAllFields();

            /*== TYPES ==*/
            schema.AddType<Models.Authenticate>("Authenticated user").AddAllFields();
            schema.AddType<Models.UserSearch>("User search with pagination").AddAllFields();

            /*== Query Types ==*/
            schema.AddType<Models.AboutServer>(nameof(Models.AboutServer), "All things about API").AddAllFields();

            /*== Query Fields ==*/
            schema.AddField("aboutServer",
               (db) => AboutServer(),
               "All things about API",
               nameof(Models.AboutServer));

            schema.AddField("searchUsers",
                new { page = 1, pagesize = 10, search = "" },
                (db, p) => PaginateUserSearch(db, p),
                "user search. [defaults: page = 1, pagesize = 10]",
                nameof(Models.UserSearch));


            /*== Directives ==*/
            schema.AddDirective("where", new GqlDirectiveSvc());

            /*== Mutations ==*/
            schema.AddMutationFrom(new GqlMutationSvc(schema, _configuration));

            return schema;
        }



        public Models.AboutServer AboutServer()
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            return new Models.AboutServer
            {
                Version = assembly.GetName().Version?.ToString(),
                VersionDate = new System.IO.FileInfo(assembly.Location).LastWriteTime
            };
        }

        private static Models.UserSearch PaginateUserSearch(DataContext db, dynamic arg)
        {
            int page = (int)arg.page;
            int pagesize = (int)arg.pagesize;
            string search = (string)arg.search;

            //Filters with defaults (could use library like; LinqKit)
            System.Linq.Expressions.Expression<Func<Entities.User, bool>> predicate = x => x.UserName != null;

            if (!string.IsNullOrEmpty(search))
                predicate = x => x.Email!.ToLower().Contains(search.ToLower());

            //Pagination
            int total = db.Users.Where(predicate).Count();
            int pagecount = ((total + pagesize) / pagesize);
            int skipTo = (page * pagesize) - (pagesize);

            var users = db.Users
                .Where(predicate)
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .Skip(skipTo)
                .Take(pagesize);

            return new Models.UserSearch
            {
                PageSize = pagesize,
                Total = total,
                Data = users
            };
        }

    }
}
