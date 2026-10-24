//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;

//namespace WebApp.FW.Controllers
//{
//    [System.Diagnostics.CodeAnalysis.SuppressMessage("Missing XML Comment", "CS1591")]
//    [RoutePrefix("api/media")]
//    public class MediaController : ApiController
//    {
//        private readonly Interfaces.IMedia _media;

//        public MediaController(Interfaces.IMedia media)
//        {
//            _media = media ?? throw new ArgumentNullException(nameof(media));
//        }




//        /// <summary>
//        /// Gets the EMC's cabinet root directory
//        /// </summary>
//        /// <returns></returns>
//        [AllowAnonymous]
//        [Route("cabinet")]
//        [HttpGet]
//        [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.OK, "Success", typeof(string))]
//        public IHttpActionResult Cabinet_GET()
//        {
//            var emcCabinet = System.Configuration.ConfigurationManager.AppSettings["EMCCabinet"];
//            return Ok(emcCabinet);
//        }

//        /// <summary>
//        /// Collection of EMC's folders
//        /// </summary>
//        /// <param name="path">folder path</param>
//        /// <returns></returns>
//        [AllowAnonymous]
//        [Route("folders")]
//        [HttpGet]
//        [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.OK, "Success", typeof(List<Emc.Documentum.Rest.DataModel.Folder>))]
//        public IHttpActionResult Get_Folder(string path = "")
//        {
//            var folders = _media.GetDirectory(path);
//            return Ok(folders);
//        }

//        /// <summary>
//        /// Collection of EMC's Documents
//        /// </summary>
//        /// <param name="path">folder path</param>
//        /// <returns></returns>
//        [AllowAnonymous]
//        [Route("files")]
//        [HttpGet]
//        [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.OK, "Success", typeof(List<Emc.Documentum.Rest.DataModel.Document>))]
//        public IHttpActionResult Files_GET(string path = "")
//        {
//            var files = _media.GetFiles(path);
//            return Ok(files);
//        }

//        /// <summary>
//        /// Upload/Removes files to/from the Project's directory
//        /// </summary>
//        /// <param name="project">name of Project</param>
//        /// <remarks>
//        /// Upload and/or removes files
//        /// deleteList = array of object_ids to be deleted
//        /// </remarks>
//        /// <returns></returns>
//        //[AccessRole(Roles = "Admin")]
//        [Route("files")]
//        [HttpPost]
//        [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.OK, "Success")]
//        [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.BadRequest, "Bad Request", typeof(Models.HttpResult))]
//        [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.UnsupportedMediaType, "Unsupported Media Type", typeof(Exception))]
//        public IHttpActionResult Post_Files(string project = null)
//        {
//            //Delete any files first
//            string list = System.Web.HttpContext.Current.Request.Form["deleteList"];

//            if (!string.IsNullOrWhiteSpace(list))
//            {
//                string[] deleteList = list.Split(',');

//                if (deleteList != null && deleteList.Length > 0)
//                    _media.RemoveFiles(deleteList);
//            }

//            if (Request.Content.IsMimeMultipartContent())
//            {
//                var fileResults = new List<Models.FileResult>();

//                var files = System.Web.HttpContext.Current.Request.Files;

//                foreach (string fileName in files)
//                {
//                    if (fileName == "deleteList")
//                        continue;

//                    System.Web.HttpPostedFile file = System.Web.HttpContext.Current.Request.Files[fileName];
//                    dynamic media = Newtonsoft.Json.JsonConvert.DeserializeObject(System.Web.HttpContext.Current.Request.Form[fileName]);
//                    media.name = fileName;

//                    if (!string.IsNullOrEmpty(project))
//                    {
//                        Emc.Documentum.Rest.DataModel.ContentMeta meta = _media.UploadFiles(file, media, project);

//                        string[] linkParts = meta.Links[(meta.Links.Count - 1)].Href.Split('/');

//                        fileResults.Add(new Models.FileResult
//                        {
//                            Name = fileName,
//                            ObjectId = linkParts[(linkParts.Length - 1)],
//                            Title = media.title
//                        });
//                    }
//                }

//                if (files.Count > 0)
//                    return Ok(fileResults);
//            }

//            //if we're' just deleting and not adding 
//            if (!string.IsNullOrWhiteSpace(list))
//                return Ok();

//            return StatusCode(HttpStatusCode.UnsupportedMediaType);
//        }

//        [Route("fileuri")]
//        [HttpGet]
//        [Swashbuckle.Swagger.Annotations.SwaggerResponse(HttpStatusCode.OK, "Success")]
//        public IHttpActionResult Get_FileUri(string id)
//        {
//            string uri = _media.GetUri(id);
//            return Ok(uri);
//        }
//    }
//}
