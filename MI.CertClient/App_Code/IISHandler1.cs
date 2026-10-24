using System;
using System.Web;
using System.Linq;

using System.Collections.Generic;

namespace MI.CertClient
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
                case "GET":
                    int Id = 0;
                    if (context.Request.QueryString["id"] != null)
                    {
                        bool hasId = Int32.TryParse(context.Request.QueryString["id"].ToString(), out Id);
                        if (!hasId)
                            Id = 0;
                    }

                    context.Response.ContentType = "application/json";
                    context.Response.Write(serializer.Serialize(new { success = true, id = Id }));
                    break;

                case "PUT":
                    string jsonModel = context.Request.Form["model"];


                    context.Response.ContentType = "application/json";
                    context.Response.Write("{\"success\": true}");
                    break;

                case "POST":
                    string formData = context.Request.Form[0];

                    System.Collections.Generic.Dictionary<String, Object> model = 
                        serializer.Deserialize<System.Collections.Generic.Dictionary<String, Object>>(formData);

                    string fullname = $"{model.Values.Last()}, {model.Values.First()}";

                    //JsonStr = serializer.Serialize(new {success = true }); // {\"success\": true}

                    context.Response.ContentType = "application/json";
                    context.Response.Write(new { fullname });
                    break;

                case "DELETE":
                    //var id = context.Request.QueryString["id"].ToString(); MUST use index?id=1
                    string id =  context.Request.Form["id"];

                    

                    context.Response.ContentType = "application/json";
                    context.Response.Write("{\"success\": true}");
                    break;
            }
        }

        #endregion
    }



}
