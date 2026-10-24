//using Microsoft.Extensions.Configuration;
//using System;

//namespace WebApp.Api.Services.Documentum
//{
//    public class DocumentumClient
//    {
//        public readonly IConfiguration _configuration;

//        private string _RestHomeUri = string.Empty;
//        private string _username = string.Empty;
//        private string _password = string.Empty;
//        private string _repositoryName = string.Empty;

//        public Emc.Documentum.Rest.Net.RestController client;
//        public Emc.Documentum.Rest.DataModel.Repository repository;

//        public DocumentumClient(IConfiguration configuration)
//        {
//            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
//            _RestHomeUri = _configuration["AppSettings:Documentum:RestURI"];
//            _username = _configuration["AppSettings:Documentum:Username"];
//            _password = _configuration["AppSettings:Documentum:Password"];
//            _repositoryName = _configuration["AppSettings:Documentum:Repository"];

//            client = String.IsNullOrEmpty(_password) ? new Emc.Documentum.Rest.Net.RestController(null, null) : new Emc.Documentum.Rest.Net.RestController(_username, _password);
//            // alternatively, you can choose .net default data contract serializer: new DefaultDataContractJsonSerializer();
//            Emc.Documentum.Rest.Net.JsonDotnetJsonSerializer serializer = new Emc.Documentum.Rest.Net.JsonDotnetJsonSerializer
//            {
//                PrintStreamBeforeDeserialize = true
//            };
//            client.JsonSerializer = serializer;

//            Emc.Documentum.Rest.DataModel.HomeDocument home = client.Get<Emc.Documentum.Rest.DataModel.HomeDocument>(_RestHomeUri, null);
//            Emc.Documentum.Rest.DataModel.Feed<Emc.Documentum.Rest.DataModel.Repository> repositories = 
//                home.GetRepositories<Emc.Documentum.Rest.DataModel.Repository>(new Emc.Documentum.Rest.Net.FeedGetOptions { Inline = true, Links = true });
//            repository = repositories.FindInlineEntry(_repositoryName);
//        }

//        public Emc.Documentum.Rest.DataModel.Feed<Emc.Documentum.Rest.DataModel.PersistentObject> ExecuteDQL(string query)
//        {
//            return repository.ExecuteDQL<Emc.Documentum.Rest.DataModel.PersistentObject>(query, new Emc.Documentum.Rest.Net.FeedGetOptions()
//                { ItemsPerPage = 50, IncludeTotal = true, Inline = true });
//        }

//        public Emc.Documentum.Rest.DataModel.Feed<Emc.Documentum.Rest.DataModel.PersistentObject> ExecuteDQL(string query, Emc.Documentum.Rest.Net.FeedGetOptions feed)
//        {
//            return repository.ExecuteDQL<Emc.Documentum.Rest.DataModel.PersistentObject>(query, feed);
//        }

//        public String RestHomeUri { get { return _RestHomeUri.Replace("/services", ""); } set { _RestHomeUri = value; } }

//        public string AuthInfo => _username + ":" + _password;
//    }
//}