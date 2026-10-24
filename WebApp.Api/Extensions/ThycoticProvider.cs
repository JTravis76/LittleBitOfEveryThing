/*=================================================
 * Code used in the Ports.Thycotic.Provider nuget
 * package. Place here at a reference.
 * 
 * If using this code outside of above packages,
 * must include the following dependencies:
 *  -- Thycotic.SecretServer.Sdk
 *  -- Thycotic.SecretServer.Sdk.Extensions.Intergration
 *  -- (v1.4.1 as of this writing)
 *================================================*/


//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.Configuration;
//using System;
//using Thycotic.SecretServer.Sdk.Areas.Secrets.Clients;
//using Thycotic.SecretServer.Sdk.Extensions.Integration.Clients;
//using Thycotic.SecretServer.Sdk.Extensions.Integration.Models;
//using Thycotic.SecretServer.Sdk.Infrastructure.Clients;
//using Thycotic.SecretServer.Sdk.Infrastructure.DataProtection;
//using Thycotic.SecretServer.Sdk.Infrastructure.Factories;
//using Thycotic.SecretServer.Sdk.Infrastructure.Loggers;
//using Thycotic.SecretServer.Sdk.Infrastructure.Providers;

//namespace Thycotic.Provider
//{
//    public static class ThycoticConfiguration
//    {
//        public static void AddThycoticConfiguration(WebHostBuilderContext context, IConfigurationBuilder builder)
//        {
//            var configuration = builder.Build();
//            builder.AddThycoticProvider(configuration);
//        }
//    }

//    public class ThycoticProvider : ConfigurationProvider
//    {
//        private ConfigSettings _thycoticConfig;
//        private SecretServerClient _thycoticClient;

//        public ThycoticProvider(IConfiguration root)
//        {
//            this._thycoticConfig = root.GetSection("Thycotic").Get<ConfigSettings>();
//            if (this._thycoticConfig == null)
//                throw new ArgumentNullException("Thycotic section missing from appsettings.json");

//            string path = null;
//            string appSetting1 = root["Thycotic:SecretServerSdkKeyDirectory"];
//            if (!string.IsNullOrWhiteSpace(appSetting1))
//            {
//                path = appSetting1;
//                if (!System.IO.Directory.Exists(path))
//                    System.IO.Directory.CreateDirectory(path);
//            }

//            //== Build a new SecretClient vs using the built-in to allow us control over the generated cache files.
//            IDataProtector idataProtector = DataProtectorFactory.Create(path);
//            string appSetting2 = root["Thycotic:SecretServerSdkConfigDirectory"];
//            ISdkClientConfigProvider sdkClientConfigProvider = new SdkClientConfigProvider(idataProtector, appSetting2);
//            ICacheClient cacheClient = new CacheClient(idataProtector, sdkClientConfigProvider);
//            IOAuthClient oAuthClient = new OAuthClient(sdkClientConfigProvider);
//            ISecretClient secretClient = new SecretClient((ILogger)new NoLogger(), (IRestClient)new RestClient(sdkClientConfigProvider), oAuthClient, cacheClient);

//            this._thycoticClient = new SecretServerClient(new NoLogger(), sdkClientConfigProvider, cacheClient, secretClient);
//            _thycoticClient.Configure(_thycoticConfig);

//            // This array contains the section names of the appsettings.json that we wish to replace.
//            string[] replacementSections = { "ConnectionStrings", "AppSettings" };

//            foreach (string section in replacementSections)
//            {
//                foreach (var item in root.GetSection(section).GetChildren())
//                {
//                    if (item.Value == null)
//                        continue;
//                    if ((!item.Value.Contains("${") ? 0 : (item.Value.Contains("?") ? 1 : 0)) == 0)
//                        continue;

//                    int secretId;
//                    var connParts = item.Value.Split('?');

//                    if (connParts.Length == 2) // Only replace if string could be split into two parts.
//                    {
//                        string itemValue = connParts[0];


//                        bool couldParseId = int.TryParse(connParts[1], out secretId);

//                        if (couldParseId) // Only replace if secret id could be parsed from string.
//                        {
//                            var secret = _thycoticClient.GetSecret(secretId);
//                            foreach (var secretItem in secret.Items)
//                            {
//                                itemValue = itemValue.Replace("${" + secretItem.FieldName + "}", secretItem.ItemValue, StringComparison.CurrentCultureIgnoreCase);
//                            }
//                            Data.Add($"{section}:{item.Key}", itemValue);
//                        }
//                    }
//                }
//            }


//        }
//    }

//    public static class ConfigurationExtensions
//    {
//        public static IConfigurationBuilder AddThycoticProvider(this IConfigurationBuilder configuration, IConfiguration root)

//        {
//            configuration.Add(new ThycoticConfigurationSource(root));
//            return configuration;
//        }
//    }

//    internal class ThycoticConfigurationSource : IConfigurationSource
//    {
//        private IConfiguration configuration;

//        public ThycoticConfigurationSource(IConfiguration configuration)
//        {
//            this.configuration = configuration;
//        }

//        public IConfigurationProvider Build(IConfigurationBuilder builder)
//        {
//            return new ThycoticProvider(configuration);
//        }
//    }
//}