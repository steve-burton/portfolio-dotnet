using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PortfolioDotnet.Models
{
    public class Projects
    {
        public static List<Projects> GetProjects()
        {
            var client = new RestClient("https://api.github.com");
            var request = new RestRequest("/users/steve-burton/starred/?client_id=" + EnvironmentVariables.ClientId + "&client_secret=" + EnvironmentVariables.ClientSecret, Method.GET);
            client.Authenticator = new HttpBasicAuthenticator(EnvironmentVariables.ClientId, EnvironmentVariables.ClientSecret);
            var response = new RestResponse();
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
            var projectList = JsonConvert.DeserializeObject<List<Projects>>(jsonResponse["starred_url"].ToString());

            return projectList;
        }

        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => 
            {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
    }
}
