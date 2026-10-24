using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WebApp.FW
{
    /// <summary>
    /// Load Assembly DLLs from a custom location outside of the magic Bin directory
    /// </summary>
    /// <see cref="https://www.strathweb.com/2013/08/customizing-controller-discovery-in-asp-net-web-api/"/>
    public class PluginResolver : System.Web.Http.Dispatcher.DefaultAssembliesResolver
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override ICollection<Assembly> GetAssemblies()
        {
            ICollection<Assembly> baseAssemblies = base.GetAssemblies();

            string path = System.Web.Hosting.HostingEnvironment.MapPath("~\\Plugins");
            string[] files = System.IO.Directory.GetFiles(path, "*.dll");

            foreach (string file in files)
            {
                Assembly controllersAssembly = Assembly.LoadFrom(file);

                foreach (Type type in controllersAssembly.GetTypes())
                {
                    if (!type.IsClass)
                        continue;

                    /*Filter the plugin based on following rules */
                    if (!type.IsAbstract && type.IsVisible && type.Name.Contains("Controller"))
                    //&& type.GetMembers().Any(x => x.DeclaringType.FullName.Equals("System.Web.Http.ApiController", StringComparison.CurrentCultureIgnoreCase)))
                    {
                        baseAssemblies.Add(controllersAssembly);
                    }
                }
            }

            return baseAssemblies;
        }

        /// <summary>
        /// 
        /// </summary>
        public string BuildXmlDocumention()
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath("~\\Plugins");
            string[] files = System.IO.Directory.GetFiles(path, "*.xml");

            var dsApi = new DataSet();
            dsApi.ReadXml(path + "\\WebApp.FW.xml");

            //member_id is auto-generated primary key
            var rowCnt = dsApi.Tables["member"].Rows.Count;
            var id = Convert.ToInt32(dsApi.Tables["member"].Rows[rowCnt - 1]["member_id"]);

            foreach (string file in files)
            {
                //Don't process the main xml file
                if (file.IndexOf("WebApp.FW.xml") == -1)
                {
                    var ds = new DataSet();
                    ds.ReadXml(file);

                    //insert member rows into the master xml
                    //increment the primary key
                    foreach (DataRow dr in ds.Tables["member"].Rows)
                    {
                        id += 1;
                        dr["member_id"] = id;
                        dsApi.Tables["member"].Rows.Add(dr.ItemArray);
                    }
                }
            }

            dsApi.WriteXml(path + "\\WebApp.FW.xml");

            return path + "\\WebApp.FW.xml";
        }

        /// <summary>
        /// Included additional XML comments from the Plugin directory
        /// </summary>
        /// <param name="config"></param>
        public static void GetXmlCommentsPath(Swashbuckle.Application.SwaggerDocsConfig config)
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath("~\\Plugins");
            string[] files = System.IO.Directory.GetFiles(path, "*.xml", System.IO.SearchOption.TopDirectoryOnly);

            foreach (string file in files)
            {
                config.IncludeXmlComments(file);
            }
        }

    }
}