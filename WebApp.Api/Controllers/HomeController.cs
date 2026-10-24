using DotLiquid;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using WebApp.Api.Interfaces;
using WebApp.Api.Models;

namespace WebApp.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        //private readonly ILogger _logger;
        private readonly IConfigurationSvc _configurationSvc;
        private readonly IEmail _emailSvc;


        public HomeController(IConfigurationSvc configurationSvc,
            IEmail emailSvc
            //, ILogger logger
        )
        {
            _configurationSvc = configurationSvc ?? throw new ArgumentNullException(nameof(configurationSvc));
            _emailSvc = emailSvc ?? throw new ArgumentNullException(nameof(emailSvc));
            //_logger = logger;
        }



        /// <summary>
        /// A simple endpoint to check if site is running
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("healthcheck")]
        public IActionResult HealthCheck()
        {
            return new OkResult();
        }


        /// <summary>
        /// Gets basic information about API
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("about")]
        [ProducesResponseType(200)]
        public IActionResult Get_About()
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var fileInfo = new System.IO.FileInfo(assembly.Location);
            string version = assembly.GetName().Version!.ToString();
            DateTime versionDate = fileInfo.LastWriteTime;

            return Ok(new { version, versionDate });
        }

        /// <summary>
        /// Sample Bad Request (400) response error
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("server400test")]
        [ProducesResponseType(typeof(IEnumerable<HttpErrorResult>), 400)]
        public IActionResult Post_FourHundredServerTest()
        {
            ModelState.Clear();
            ModelState.AddModelError("Name", "Some random vaildation error.");

            IEnumerable<HttpErrorResult> errors = ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(error => (HttpErrorResult)error);
            return BadRequest(errors);
        }

        /// <summary>
        /// Sample Internal Server (500) response error
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("server500test")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult Post_FiveHundredServerTest()
        {
            string result = "Hello World";

            return Ok(result.Substring(12));
        }

        /// <summary>
        /// Sample Email Test
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("emailtest")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Post_EmailTest()
        {
            var mailMessage = new MailMessage
            {
                Subject = "Test Email",
                Body = "This is a test from the national broadcasting system",
                From = new MailAddress("no-reply@domain.net"),
                IsBodyHtml = true
            };

            //var template = _emailSvc.GetEmailTemplate(Environment.CurrentDirectory, "Templates/Email/ServiceRequest.liquid");
            //var content = template.Render(Hash.FromAnonymousObject(new
            //{
            //    ReportedByName = "",
            //    TicketId = "",
            //    Description = "",
            //    ReportDate = DateTime.Now.ToString("yyyy-MM-dd"),
            //    ApproveLink = "",
            //    RejectLink = ""
            //}));
            //mailMessage.Body = content;

            // if (Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT") == "Development") {}

            return Ok(await _emailSvc.SendEmail(mailMessage));
        }

        /// <summary>
        /// Logs an test in the Event Viewer
        /// </summary>
        /// <param name="id">Id to include in the log</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("logtest")]
        [ProducesResponseType(200)]
        public IActionResult Post_LoggerTest([FromQuery] int id = 0)
        {
            //_logger.LogInformation(LoggingEvents.LoggingTest, "This is a test log passing an ID of {ID}", id);
            return Ok();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("/configurationlist")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<Entities.Configuration>), 200)]
        public async Task<IActionResult> Get_ConfigurationsAsync()
        {
            return new JsonResult(await _configurationSvc.GetConfigurationsAsync());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("/configurations")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Models.ConfigurationPager), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> Get_ConfigurationsAsync(int page = 1, int pagesize = 25, string search = "")
        {
            try
            {
                return new JsonResult(await _configurationSvc.GetConfigurationPager(page, pagesize, search));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        [HttpPost("/configuration")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Entities.Configuration), 200)]
        [ProducesResponseType(typeof(IEnumerable<HttpErrorResult>), 400)]
        public async Task<IActionResult> Post_ConfigurationAsync(Entities.Configuration configuration)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    configuration.ModifiedBy = Request.HttpContext.User.Identity!.Name;

                    return new JsonResult(await _configurationSvc.UpdateConfiguration(configuration));
                }
                else
                {
                    var errors = ModelState.Values
                        .SelectMany(x => x.Errors)
                        .Select(z => (Models.HttpErrorResult)z);
                    return BadRequest(errors);
                }
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex.Message);
            }
        }
    }
}
