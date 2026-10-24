using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.FW.App_Start;
using WebApp.FW.Interfaces;

namespace WebApp.FW.Controllers
{
    /// <summary>
    /// Home Route
    /// </summary>
    [RoutePrefix("api/home")]
    public class HomeController : ApiControllerExt //ApiController
    {
        private readonly Interfaces.IRepository _repository;

        public HomeController(Interfaces.IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }



        /// <summary>
        /// Some build information about the API
        /// </summary>
        /// <remarks>
        /// Basic build information about this API
        /// </remarks>
        /// <returns>AboutDto</returns>
        [AllowAnonymous]
        [Route("about")]
        [HttpGet]
        [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.OK, "Success", typeof(Models.About))]
        public IHttpActionResult Get_About()
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var fileInfo = new System.IO.FileInfo(assembly.Location);

            return Ok(new Models.About
            {
                Version = assembly.GetName().Version.ToString(),
                VersionDate = fileInfo.LastWriteTime
            });
        }

        /// <summary>
        /// Sample Bad Request (400) response error
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("server400test")]
        [HttpPost]
        [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.BadRequest, "Bad Request", typeof(Models.HttpResult))]
        public IHttpActionResult Post_FourHundredServerTest()
        {
            ModelState.Clear();
            ModelState.AddModelError("Name", "Some random vaildation error.");

            IEnumerable<Models.HttpResult> error = ModelState.Values.SelectMany(x => x.Errors).Select(z => (Models.HttpResult)z);
            return Content(HttpStatusCode.BadRequest, error);
        }

        /// <summary>
        /// Sample Internal Server (500) response error
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("server500test")]
        [HttpPost]
        [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.InternalServerError, "Internal Server Error")]
        public IHttpActionResult Post_FiveHundredServerTest()
        {
            using (var repo = _repository)
            {
                var result = repo.Read<Entities.Configuration>(x => x.ConfigName.Equals(""));
                return Json(result);
            }

            //return Content(HttpStatusCode.InternalServerError, "Random server error");
        }

        /// <summary>
        /// Get Slicer information for dashboard charts
        /// </summary>
        /// <remarks>
        /// Roles: none
        /// </remarks>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("slicers")]
        [HttpGet]
        public IHttpActionResult Get_Slicers()
        {
            // Create some Anonymous Types to store data
            var data = Enumerable.Empty<object>().Select(x => new { text = "", value = "", selected = false }).ToList();
            var slicers = Enumerable.Empty<object>().Select(x => new { active = false, data, title = "", type = "" }).ToList();

            using (var repo = _repository)
            {
                //== Get [Cell] from Configuration Settings
                var cell = repo.Read<Entities.Configuration>(x => x.ConfigName == "Cell")
                    .FirstOrDefault();

                if (cell != null && cell.ConfigValue != null)
                {
                    //1st convert string to string array, then cast into a List of Anonymous Types, finally add to slicers
                    var cells = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(cell.ConfigValue)
                        .Select(x => new
                        {
                            text = x,
                            value = x,
                            selected = false
                        })
                        .ToList();
                    slicers.Add(new { active = false, data = cells, title = "Cells", type = "checkbox" });
                }
            }

            return Ok(slicers);
        }

        /// <summary>
        /// Picker list of Configuration
        /// </summary>
        /// <param name="configName">name of configuration</param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("config")]
        [HttpGet]
        [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.OK, "Success", typeof(List<Models.Picker>))]
        public IHttpActionResult Get_ConfigurationPicker(string configName)
        {
            using (var repo = _repository)
            {
                var model = new List<Models.Picker>();

                var config = repo.Read<Entities.Configuration>(x => x.ConfigName.Equals(configName, StringComparison.CurrentCultureIgnoreCase))
                    .FirstOrDefault();

                if (config != null)
                {
                    if (config.ConfigType == "Array")
                    {
                        string[] values = Newtonsoft.Json.JsonConvert.DeserializeObject<string[]>(config.ConfigValue);

                        model = values
                           .Select(x => new Models.Picker { Value = x, Text = x, Selected = false })
                           .OrderBy(x => x.Text)
                           .ToList();
                    }
                    else if (config.ConfigType == "Object")
                    {
                        //var definition = new { Name = "", Value = "" };
                        var definition = Enumerable.Empty<object>().Select(x => new { Name = "", Value = "" });

                        var values = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(config.ConfigValue, definition);

                        model = values
                           .Select(x => new Models.Picker { Value = x.Value, Text = x.Name, Selected = false })
                           .OrderBy(x => x.Text)
                           .ToList();
                    }
                }

                return Ok(model);
            }
        }

        /// <summary>
        /// Paginate through a collection of Configurations
        /// </summary>
        /// <param name="page">page number (default is 1)</param>
        /// <param name="pagesize">number of records to return (default is 25)</param>
        /// <param name="search">text to search</param>
        /// <remarks>
        /// Searching on: Configuration Name
        /// Roles: Admin
        /// </remarks>
        /// <returns></returns>
        [AccessRole(Roles = "Admin")]
        [Route("configs")]
        [HttpGet]
        [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.OK, "Success", typeof(Models.ConfigurationPagination))]
        public IHttpActionResult Config_GET(int page = 1, int pagesize = 25, string search = "")
        {
            using (var repo = _repository)
            {
                var predicate = LinqKit.PredicateBuilder.New<Entities.Configuration>(x => x.ConfigName != null);

                if (!string.IsNullOrEmpty(search))
                    predicate.And(x => x.ConfigName.Contains(search));

                //Pagination
                int cnt = repo.Read<Entities.Configuration>(predicate).Count();
                int pagecount = ((cnt + pagesize) / pagesize);
                int skipTo = (page * pagesize) - (pagesize);

                var configs = repo.Read<Entities.Configuration>()
                    .OrderBy(x => x.ConfigName)
                    .Skip(skipTo)
                    .Take(pagesize)
                    .ToList();

                return Ok(new Models.ConfigurationPagination
                {
                    Page = page,
                    PageCount = pagecount,
                    Data = configs
                });
            }
        }

        /// <summary>
        /// Updates a Configuration setting
        /// </summary>
        /// <param name="config">Configuration</param>
        /// <remarks>
        /// There is (3) types of configurations: String(NULL), Object, or Array.
        /// String - can be use either to set a string value for a setting or as a on/off feature of a setting.
        /// Object - use a key/pair value for a list. [{"Name": "", "Value": ""}]
        /// Array - a list of strings. ["String1", "String2"]
        /// Roles: Admin
        /// </remarks>
        /// <returns></returns>
        [AccessRole(Roles = "Admin")]
        [Route("config")]
        [HttpPost]
        [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.OK, "Success", typeof(Entities.Configuration))]
        [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.BadRequest, "Bad Request", typeof(List<Models.HttpResult>))]
        public IHttpActionResult Config_POST(Entities.Configuration config)
        {
            if (config.ConfigType == "Array")
                config.ConfigStatus = null;

            if (ModelState.IsValid)
            {
                using (var repo = _repository)
                {
                    config.ModifiedBy = RequestContext.Principal.Identity.Name;
                    config.ModifiedDttm = DateTime.Now;

                    repo.Update(config);
                    repo.Commit();

                    return Ok(config);
                }
            }
            IEnumerable<Models.HttpResult> error = ModelState.Values.SelectMany(x => x.Errors).Select(z => (Models.HttpResult)z);
            return Content(HttpStatusCode.BadRequest, error);
        }

        /// <summary>
        /// PDF of Project Planning Checklist, FBP-FRM-01127
        /// </summary>
        /// <param name="id">id of Project Planning Checklist</param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("projectplan")]
        [HttpGet]
        public IHttpActionResult Get_PdfReport()
        {
            Entities.ProjectPlan projectPlan = null;

            //Pass some data to create a PDF blob.  
            byte[] PDF = new PDF.Creation().FRM_01127(projectPlan);
            // To use 'FileResult', replace 'ApiController' above with 'ApiControllerExt'.
            return FileResult(PDF);
        }

    }
}
