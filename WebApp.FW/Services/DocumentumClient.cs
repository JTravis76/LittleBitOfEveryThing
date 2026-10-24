using System;
using System.Web.Configuration;

namespace WebApp.FW.Services
{
    public class DocumentumClient
    {
        private string _RestHomeUri = WebConfigurationManager.AppSettings["EmcServiceBase"]; // "#EXAMPLE: http://apps.company.com/dctm-rest):";
        private readonly string _username = WebConfigurationManager.AppSettings["EmcUsername"]; //"#EXAMPLE: dmadmin";
        private readonly string _password = WebConfigurationManager.AppSettings["EmcPassword"]; //"#EXAMPLE: MyS^p3Cc3P@55W0rd!";
        private readonly string _repositoryName = WebConfigurationManager.AppSettings["EmcRepository"]; //"#EXAMPLE: CompanyDocbase";

        //public readonly Emc.Documentum.Rest.Net.RestController client;
        //public readonly Emc.Documentum.Rest.DataModel.Repository repository;

        //public DocumentumClient()
        //{
        //    client = String.IsNullOrEmpty(_password) ? new Emc.Documentum.Rest.Net.RestController(null, null) : new Emc.Documentum.Rest.Net.RestController(_username, _password);
        //    // alternatively, you can choose .net default data contract serializer: new DefaultDataContractJsonSerializer();
        //    Emc.Documentum.Rest.Net.JsonDotnetJsonSerializer serializer = new Emc.Documentum.Rest.Net.JsonDotnetJsonSerializer
        //    {
        //        PrintStreamBeforeDeserialize = true
        //    };
        //    client.JsonSerializer = serializer;

        //    Emc.Documentum.Rest.DataModel.HomeDocument home = client.Get<Emc.Documentum.Rest.DataModel.HomeDocument>($"{_RestHomeUri}/services", null);
        //    Emc.Documentum.Rest.DataModel.Feed<Emc.Documentum.Rest.DataModel.Repository> repositories =
        //        home.GetRepositories<Emc.Documentum.Rest.DataModel.Repository>(new Emc.Documentum.Rest.Net.FeedGetOptions { Inline = true, Links = true });
        //    repository = repositories.FindInlineEntry(_repositoryName);
        //}

        //public Emc.Documentum.Rest.DataModel.Feed<Emc.Documentum.Rest.DataModel.PersistentObject> ExecuteDQL(string query)
        //{
        //    return repository.ExecuteDQL<Emc.Documentum.Rest.DataModel.PersistentObject>(query, new Emc.Documentum.Rest.Net.FeedGetOptions()
        //    { ItemsPerPage = 50, IncludeTotal = true, Inline = true });
        //}

        //public Emc.Documentum.Rest.DataModel.Feed<Emc.Documentum.Rest.DataModel.PersistentObject> ExecuteDQL(string query, Emc.Documentum.Rest.Net.FeedGetOptions feed)
        //{
        //    return repository.ExecuteDQL<Emc.Documentum.Rest.DataModel.PersistentObject>(query, feed);
        //}

        public String RestHomeUri { get { return _RestHomeUri.Replace("/services", ""); } set { _RestHomeUri = value; } }

        public string AuthInfo => _username + ":" + _password;
    }
}