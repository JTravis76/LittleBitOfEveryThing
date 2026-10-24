using System;
using System.Web;
using System.Linq;

using System.Collections.Generic;

namespace ApplicationGallery
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

            //get data from home-grown XML database ;)
            System.Data.DataSet ds = new System.Data.DataSet();
            
            switch (HttpContext.Current.Request.RequestType)
            {              
                case "GET":
                    ds.ReadXml(HttpContext.Current.Request.MapPath("~/App_Data/db.xml"));

                    List<Applications> model = new List<Applications>();

                    try
                    {
                        int r = ds.Tables["Applications"].Rows.Count;
                    }
                    catch (Exception)
                    {
                        //reset sequence
                        ds.Tables["sequence"].Rows[0]["nextSeq"] = 1;
                        ds.WriteXml(HttpContext.Current.Request.MapPath("~/App_Data/db.xml"));

                        //create blank table to satifiy data schema
                        System.Data.DataTable dt = new System.Data.DataTable("Applications");
                        dt.Columns.Add("id");
                        dt.Columns.Add("name");
                        dt.Columns.Add("title");
                        dt.Columns.Add("link");
                        dt.Columns.Add("img");

                        System.Data.DataRow dr = dt.NewRow();
                        dr["id"] = 1;
                        dr["name"] = "";
                        dr["link"] = "";
                        dr["img"] = "";

                        ds.Tables.Add(dt);                        
                    }
                    

                    //fill list with data
                    foreach (System.Data.DataRow dr in ds.Tables["Applications"].Rows)
                    {
                        model.Add(new Applications
                        {
                            id = Convert.ToInt32(dr["id"]),
                            name = dr["name"].ToString(),
                            title = dr["title"].ToString(),
                            link = dr["link"] == null? "": dr["link"].ToString(),
                            img = dr["img"].ToString()
                        });
                    }

                    //IF GetById in pass in
                    if (context.Request.QueryString["id"] != null)
                    {
                        int GetId = Convert.ToInt32( context.Request.QueryString["id"].ToString() );
                        model = model
                            .Where(x => x.id == GetId)
                            .ToList();
                    }
                    
                    //Convert to JSON string
                    string jsonStr = serializer.Serialize(model.OrderBy(x => x.name).ToList());

                    context.Response.ContentType = "text/json";
                    context.Response.Write(jsonStr);
                    break;

                case "PUT":
                    ds.ReadXml(HttpContext.Current.Request.MapPath("~/App_Data/db.xml"));

                    string jsonModel = context.Request.Form["model"];
                    if(jsonModel != null)
                    {
                        Applications app = serializer.Deserialize<Applications>(jsonModel);

                        foreach (System.Data.DataRow dr in ds.Tables["Applications"].Rows)
                        {
                            if(Convert.ToInt32(dr[0]) == app.id)
                            {
                                dr["name"] = app.name;
                                dr["title"] = app.title;
                                dr["img"] = app.img;
                                dr["link"] = app.link;

                                ds.WriteXml(HttpContext.Current.Request.MapPath("~/App_Data/db.xml"));
                                break;
                            }
                        }
                    }

                    context.Response.ContentType = "text/json";
                    context.Response.Write("{\"success\": true}");
                    break;

                case "POST":
                    ds.ReadXml(HttpContext.Current.Request.MapPath("~/App_Data/db.xml"));

                    try
                    {
                        int r = ds.Tables["Applications"].Rows.Count;
                    }
                    catch
                    {
                        System.Data.DataTable dt = new System.Data.DataTable("Applications");
                        dt.Columns.Add("id");
                        dt.Columns.Add("name");
                        dt.Columns.Add("title");
                        dt.Columns.Add("link");
                        dt.Columns.Add("img");

                        ds.Tables.Add(dt);
                    }



                    string JsonStr = context.Request.Form["model"];
                    if (JsonStr != null)
                    {
                        Applications app = serializer.Deserialize<Applications>(JsonStr);

                        //get new sequence ID
                        int seq = Convert.ToInt32(ds.Tables["sequence"].Rows[0]["nextSeq"]);

                        System.Data.DataRow dr = ds.Tables["Applications"].NewRow();
                        dr["id"] = seq;
                        dr["name"] = app.name;
                        dr["title"] = app.title;
                        dr["link"] = app.link;
                        dr["img"] = app.img;

                        ds.Tables["Applications"].Rows.Add(dr);

                        ds.Tables["sequence"].Rows[0]["nextSeq"] = (seq + 1).ToString();
                        ds.WriteXml(HttpContext.Current.Request.MapPath("~/App_Data/db.xml"));
                    }

                    /*== User Authentication section ==*/
                    string username = context.Request.Form["username"];
                    string password = context.Request.Form["password"];
                    string authenticated = "{\"auth\": false}";

                    if(username != null && password != null)
                    {
                        AES aes = new AES();
                        //if current password is blank, set to new password
                        if(ds.Tables["Users"].Rows[0]["pwd"].ToString() == "")
                        {
                            string newPwd = aes.Encrypt(password, Strength.Good);
                            ds.Tables["Users"].Rows[0]["pwd"] = newPwd;
                            ds.WriteXml(HttpContext.Current.Request.MapPath("~/App_Data/db.xml"));
                        }
                       
                        string pwd = ds.Tables["Users"].Rows[0]["pwd"].ToString();
                        string decrypted = aes.Decrypt(pwd, Strength.Good);

                        if (username == ds.Tables["Users"].Rows[0]["username"].ToString() && 
                            password == decrypted)
                        {
                            authenticated = "{\"auth\": true}";
                        }
                    }


                    //JsonStr = serializer.Serialize(new {success = true }); // {\"success\": true}

                    context.Response.ContentType = "text/json";
                    context.Response.Write(authenticated);
                    break;

                case "DELETE":
                    //var id = context.Request.QueryString["id"].ToString(); MUST use index?id=1
                    string id =  context.Request.Form["id"];

                    ds.ReadXml(HttpContext.Current.Request.MapPath("~/App_Data/db.xml"));
                    foreach (System.Data.DataRow dr in ds.Tables["Applications"].Rows)
                    {
                        if(dr["id"].ToString() == id)
                        {
                            dr.Delete();
                            ds.WriteXml(HttpContext.Current.Request.MapPath("~/App_Data/db.xml"));
                            break;
                        }
                    }

                    context.Response.ContentType = "text/json";
                    context.Response.Write("{\"success\": true}");
                    break;
            }



        }

        #endregion
    }


    public class Applications
    {
        public int id { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string link { get; set; }
        public string img { get; set; }
    }

    public class Users
    {
        public string username { get; set; }
        public string pwd { get; set; }
    }
}
