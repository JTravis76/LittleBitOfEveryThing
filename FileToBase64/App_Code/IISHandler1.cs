using System;
using System.Web;
using System.Linq;

using System.Collections.Generic;
using System.Activities.Expressions;

namespace FileToBase64
{
    public class IISHandler1 : IHttpHandler
    {
        /// <summary>
        /// You will need to configure this handler in the Web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public IISHandler1() { }

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            switch (HttpContext.Current.Request.RequestType)
            {
                case "OPTIONS":
                    context.Response.AddHeader("access-control-allow-origin", "*");
                    context.Response.ContentType = "text/plain";
                    context.Response.StatusCode = 201;
                    context.Response.Write("");
                    break;
                case "GET":
                    string vApp = context.Request.QueryString["vApp"];
                    string fileName = context.Request.QueryString["fileName"];

                    string root = HttpContext.Current.Server.MapPath("~");
                    string filePath = System.IO.Path.Combine(root, vApp, fileName);
                    string fileContent = "";
                    if (System.IO.File.Exists(filePath))
                    {
                        Byte[] bytes = System.IO.File.ReadAllBytes(filePath);
                        fileContent = "data:application/" + fileName.Split('.')[1].ToString() + ";base64," + Convert.ToBase64String(bytes);
                    }
                    context.Response.ContentType = "plain/text";
                    context.Response.Write(fileContent);
                    break;

                case "PUT":
                    context.Response.ContentType = "text/json";
                    context.Response.Write("{\"success\": true}");
                    break;

                case "POST":
                    string jsonStr = serializer.Serialize(new { success = true }); // {\"success\": true}

                    context.Response.ContentType = "text/json";
                    context.Response.Write(jsonStr);
                    break;

                case "DELETE":
                    // MUST use index?id=1
                    string id = context.Request.Form["id"];

                    context.Response.ContentType = "text/json";
                    context.Response.Write("{\"success\": true}");
                    break;

                default:
                    break;
            }



        }

        #endregion
    }
}
