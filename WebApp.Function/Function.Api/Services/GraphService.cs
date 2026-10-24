using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebAppFunction.Api.Classes;
using WebAppFunction.Api.Models;
using WebAppFunction.Api.Services.Interfaces;

namespace WebAppFunction.Api.Services
{
    public class GraphService : IGraphService
    {
        private static readonly string clientId = EnvironmentVariables.ClientId.Get();
        private static readonly string tenantId = EnvironmentVariables.TenantId.Get();
        private static readonly string clientSecret = EnvironmentVariables.ClientSecret.Get();

        public GraphService() { }

        public async Task<GraphUserModel> Users()
        {
            var auth = await ClientCredentialAsync();

            var url = new Uri("https://graph.microsoft.com/v1.0" + "/users" + "?$select=DisplayName,Id,Mail,Surname,GivenName");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", auth.AccessToken);
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<GraphUserModel>(result);
            }
            return new GraphUserModel();
        }

        private static async Task<GraphAuthenticationModel> ClientCredentialAsync()
        {
            var formData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("scope", "https://graph.microsoft.com/.default")
            };

            byte[] encode = System.Text.Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}");
            string authCode = Convert.ToBase64String(encode);

            var url = new Uri($"https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/token");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authCode);
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(formData)
            };
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<GraphAuthenticationModel>(result);
            }
            return new GraphAuthenticationModel();
        }

    }
}
