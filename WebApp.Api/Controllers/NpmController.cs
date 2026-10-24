using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Buffers;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using WebApp.Api.Database;
using WebApp.Api.Models;

namespace WebApp.Api.Controllers
{
    /// <summary>
    /// This is the default root routing
    /// </summary>
    [Route("")]
    [ApiController]
    public class NpmController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly DataContext _dataContext;
        private readonly IConfiguration _config;

        private static string _contentRootPath = "";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataContext"></param>
        /// <param name="webHostEnvironment"></param>
        /// <param name="config"></param>
        public NpmController(DataContext dataContext, IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            _dataContext = dataContext;
            _webHostEnvironment = webHostEnvironment;
            _config = config;

            _contentRootPath = webHostEnvironment.ContentRootPath;
        }


        /// <summary>
        /// Search results of NPM packages
        /// </summary>
        /// <param name="search">package name to search upon</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("search")]
        [ProducesResponseType(typeof(IQueryable<Entities.Package>), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        public IActionResult Search([FromBody] string? search = null)
        {
            if (!string.IsNullOrEmpty(search))
            {
                // == File-System Section
                //string dbPath = $@"{_contentRootPath}\NPM-Packages\database.json";
                //string data = System.IO.File.ReadAllText(dbPath);
                //var db = System.Text.Json.JsonSerializer
                //    .Deserialize<List<BusinessObjects.Entities.Package>>(data)
                //    ?? new List<BusinessObjects.Entities.Package>() { };

                //db = db.Where(x => x.Name != null && x.Name.ToLower()
                //    .Contains(search.ToLower()))
                //    .OrderBy(x => x.Name)
                //    .ToList();

                //return Ok(db);

                // == Database Section
                var packages = _dataContext.Packages
                    .Where(x => x.Name != null && x.Name.Contains(search));
                return Ok(packages);
            }

            return Ok(Array.Empty<string>());
        }

        #region NPM Package Information

        /// <summary>
        /// Fetch the package detail information to display
        /// </summary>
        /// <param name="name">package name</param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("{name}")]
        [HttpGet]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult GET_PackageDetails(string name)
        {
            string filePath = $@"{_contentRootPath}NPM-Packages\{name}\{name}.json";

            if (name != null && name.StartsWith("@") && name.Contains("%2f"))
            {
                string[] values = name.Split("%2f");
                filePath = $@"{_contentRootPath}NPM-Packages\{values[0]}\{values[1]}\{values[1]}.json";
            }

            if (System.IO.File.Exists(filePath))
            {
                // Since this is a JSON file, the string has already been serialized.
                string text = System.IO.File.ReadAllText(filePath);
                return Ok(text);
            }

            return BadRequest("Package doesn't exist");
        }

        /// <summary>
        /// Fetch the package detail information to display
        /// </summary>
        /// <param name="org">organization name</param>
        /// <param name="name">package name</param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("{org}/{name}")]
        [HttpGet]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult GET_OrgPackageDetails(string org, string name)
        {
            string filePath = $@"{_contentRootPath}\NPM-Packages\{org}\{name}\{name}.json";

            if (System.IO.File.Exists(filePath))
            {
                // Since this is a JSON file, the string has already been serialized.
                string text = System.IO.File.ReadAllText(filePath);
                return Ok(text);
            }

            return BadRequest("Package doesn't exist");
        }

        /// <summary>
        /// Downloads the desired NPM package
        /// </summary>
        /// <param name="name">package name</param>
        /// <param name="package">also the package name</param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("{name}/-/{package}")]
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Package_GET(string name, string package)
        {
            byte[]? file = null;

            using (System.IO.FileStream fs = System.IO.File.OpenRead($@"{_contentRootPath}\NPM-Packages\{name}\{package}"))
            {
                //Read into bytes so I can dispose the stream
                file = new byte[fs.Length];
                fs.Read(file, 0, file.Length);
            }
            return File(file, "application/octet-stream", package);
        }

        /// <summary>
        /// Downloads the desired NPM organization package
        /// </summary>
        /// <param name="org">organization name</param>
        /// <param name="org1">organization name</param>
        /// <param name="name">name of package</param>
        /// <param name="package">package name</param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("{org}/{name}/-/{org1}/{package}")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        public IActionResult OrgPackage_GET(string org, string org1, string name, string package)
        {
            byte[]? file = null;

            string filePath = $@"{_contentRootPath}\NPM-Packages\{org}\{name}\{package}";

            if (System.IO.File.Exists(filePath))
            {
                using (System.IO.FileStream fs = System.IO.File.OpenRead(filePath))
                {
                    //Read into bytes so I can dispose the stream
                    file = new byte[fs.Length];
                    fs.Read(file, 0, file.Length);
                }
                return File(file, "application/octet-stream", package);
            }

            return BadRequest($"Package {package} is not available");
        }

        #endregion

        #region NPM Authorize Actions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jObj"></param>
        /// <returns></returns>
        [Authorize]
        [Route("{name}")]
        [Route("{org}/{name}")]
        [HttpPut]
        public IActionResult PUT_Package(dynamic jObj)//(Newtonsoft.Json.Linq.JObject jObj)
        {
            string packageName = jObj.SelectToken("name").ToString();
            string version = jObj.SelectToken("dist-tags.latest").ToString();
            string description = jObj.SelectToken("description").ToString();
            string encodedData = jObj.SelectToken("_attachments.*.data").ToString();
            string directory = $@"{_contentRootPath}\NPM-Packages\{packageName}";

            // clear the attachment before saving JSON
            jObj.SelectToken("_attachments").Replace(System.Text.Json.Nodes.JsonObject.Parse("{}"));

            // might have scope value; @org/package
            if (packageName.StartsWith("@") && packageName.Contains("/"))
            {
                string[] values = packageName.Split("/");
                packageName = values[1];
                directory = $@"{_contentRootPath}\NPM-Packages\{values[0]}\{packageName}";
            }

            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);

                // Add Timestamps
                var time = new System.Text.Json.Nodes.JsonObject
                        {
                            { "created", DateTime.Now },
                            { "modified", DateTime.Now },
                            { version, DateTime.Now }
                        };
                jObj.Add("time", time);

                System.IO.File.WriteAllText($@"{directory}\{packageName}.json", jObj.ToString());
            }
            else
            {
                // Add Timestamps for this version to be merged later
                jObj.Add("time", new System.Text.Json.Nodes.JsonObject { { version, DateTime.Now } });


                string jsonFile = $"{directory}\\{packageName}.json";
                if (System.IO.File.Exists(jsonFile))
                {
                    string fileContent = System.IO.File.ReadAllText(jsonFile);
                    var originalJson = System.Text.Json.JsonSerializer.Deserialize<dynamic>(fileContent);
                    //var originalJson = System.Text.Json.Nodes.JsonObject.Parse(fileContent);
                    var versions = originalJson!.SelectTokens("versions.*.version");

                    if (versions.ToList().Contains(version))
                    {
                        return StatusCode(400, new { ok = "package already exists", success = false });
                    }
                    else
                    {
                        // TODO: This needs tested !!
                        Merge(originalJson, jObj);
                        //originalJson.Merge(jObj, new Newtonsoft.Json.Linq.JsonMergeSettings
                        //{
                        //    MergeArrayHandling = Newtonsoft.Json.Linq.MergeArrayHandling.Union
                        //});

                        System.IO.File.WriteAllText($@"{directory}\{packageName}.json", originalJson.ToString());
                    }
                }
                else
                {
                    return StatusCode(500, "cannot find package json to merge");
                }
            }

            System.IO.File.WriteAllBytes($@"{directory}\{packageName}-{version}.tgz", Convert.FromBase64String(encodedData));

            // == Database section
            string fullPackageName = jObj.SelectToken("name").ToString();
            var p = _dataContext.Packages.Find(fullPackageName);
            if (p == null)
                _dataContext.Add(new Entities.Package
                {
                    Name = fullPackageName,
                    Description = description,
                    Version = version
                });
            else
            {
                p.Version = version;
                p.Description = description;
                _dataContext.Update(p);
            }
            _dataContext.SaveChanges();

            return StatusCode(201, new { ok = "created new package", success = true });
        }

        /// <summary>
        /// This is a NPM command to add a user. Nothing is stored.
        /// Only returned a authorize token to add/update packages
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("-/user/org.couchdb.user:{user}")]
        [HttpPut]
        public IActionResult PUT_Login(Models.NpmUser user)
        {
            if (user != null && user.Name != null)
            {
                var claims = new List<Claim>
                        {
                            new Claim("AuthDate", user.Date.ToString()),
                            //new Claim(JwtRegisteredClaimNames.Email, ""),
                            new Claim(ClaimTypes.Name, user.Name) //<- User.Identity.Name
                            //new Claim("CustomClaim", "myCustomValue")
                        };

                foreach (string role in user.Roles ?? Array.Empty<string>())
                    claims.Add(new Claim(ClaimTypes.Role, role));

                // == Create the token
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var jwt = new JwtSecurityToken(_config["Jwt:Issuer"],
                        _config["Jwt:Issuer"],
                        claims,
                        expires: DateTime.Now.AddMonths(3),
                        signingCredentials: credentials);

                string token = new JwtSecurityTokenHandler().WriteToken(jwt);

                return Ok(new { ok = $"user '{user?.Name}' created", token });
            }

            return BadRequest("could not create user");
        }

        /// <summary>
        /// Who Am I? A nice way to test if Authentication/Authorization is working
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("-/whoami")]
        [HttpGet]
        public IActionResult GET_Whoami()
        {
            return Ok(new { username = User.Identity?.Name });
        }

        #endregion


        #region Merge E

        /// <summary>
        /// 
        /// </summary>
        /// <param name="originalJson"></param>
        /// <param name="newContent"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static string Merge(string originalJson, string newContent)
        {
            var outputBuffer = new ArrayBufferWriter<byte>();

            using (JsonDocument jDoc1 = JsonDocument.Parse(originalJson))
            using (JsonDocument jDoc2 = JsonDocument.Parse(newContent))
            using (var jsonWriter = new Utf8JsonWriter(outputBuffer, new JsonWriterOptions { Indented = true }))
            {
                JsonElement root1 = jDoc1.RootElement;
                JsonElement root2 = jDoc2.RootElement;

                if (root1.ValueKind != JsonValueKind.Array && root1.ValueKind != JsonValueKind.Object)
                {
                    throw new InvalidOperationException($"The original JSON document to merge new content into must be a container type. Instead it is {root1.ValueKind}.");
                }

                if (root1.ValueKind != root2.ValueKind)
                {
                    return originalJson;
                }

                if (root1.ValueKind == JsonValueKind.Array)
                {
                    MergeArrays(jsonWriter, root1, root2);
                }
                else
                {
                    MergeObjects(jsonWriter, root1, root2);
                }
            }

            return Encoding.UTF8.GetString(outputBuffer.WrittenSpan);
        }

        private static void MergeObjects(Utf8JsonWriter jsonWriter, JsonElement root1, JsonElement root2)
        {
            Debug.Assert(root1.ValueKind == JsonValueKind.Object);
            Debug.Assert(root2.ValueKind == JsonValueKind.Object);

            jsonWriter.WriteStartObject();

            // Write all the properties of the first document.
            // If a property exists in both documents, either:
            // * Merge them, if the value kinds match (e.g. both are objects or arrays),
            // * Completely override the value of the first with the one from the second, if the value kind mismatches (e.g. one is object, while the other is an array or string),
            // * Or favor the value of the first (regardless of what it may be), if the second one is null (i.e. don't override the first).
            foreach (JsonProperty property in root1.EnumerateObject())
            {
                string propertyName = property.Name;

                JsonValueKind newValueKind;

                if (root2.TryGetProperty(propertyName, out JsonElement newValue) && (newValueKind = newValue.ValueKind) != JsonValueKind.Null)
                {
                    jsonWriter.WritePropertyName(propertyName);

                    JsonElement originalValue = property.Value;
                    JsonValueKind originalValueKind = originalValue.ValueKind;

                    if (newValueKind == JsonValueKind.Object && originalValueKind == JsonValueKind.Object)
                    {
                        MergeObjects(jsonWriter, originalValue, newValue); // Recursive call
                    }
                    else if (newValueKind == JsonValueKind.Array && originalValueKind == JsonValueKind.Array)
                    {
                        MergeArrays(jsonWriter, originalValue, newValue);
                    }
                    else
                    {
                        newValue.WriteTo(jsonWriter);
                    }
                }
                else
                {
                    property.WriteTo(jsonWriter);
                }
            }

            // Write all the properties of the second document that are unique to it.
            foreach (JsonProperty property in root2.EnumerateObject())
            {
                if (!root1.TryGetProperty(property.Name, out _))
                {
                    property.WriteTo(jsonWriter);
                }
            }

            jsonWriter.WriteEndObject();
        }

        private static void MergeArrays(Utf8JsonWriter jsonWriter, JsonElement root1, JsonElement root2)
        {
            Debug.Assert(root1.ValueKind == JsonValueKind.Array);
            Debug.Assert(root2.ValueKind == JsonValueKind.Array);

            jsonWriter.WriteStartArray();

            // Write all the elements from both JSON arrays
            foreach (JsonElement element in root1.EnumerateArray())
            {
                element.WriteTo(jsonWriter);
            }
            foreach (JsonElement element in root2.EnumerateArray())
            {
                element.WriteTo(jsonWriter);
            }

            jsonWriter.WriteEndArray();
        }

        #endregion
    }
}
